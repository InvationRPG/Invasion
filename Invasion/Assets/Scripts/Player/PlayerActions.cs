using UnityEngine;
using Mirror;
using System.Collections;

public class PlayerActions : NetworkBehaviour
{
    [SerializeField] private AnimationCurve _cameraZoomSetting;

    private bool _isZoom;
    private Transform pos;

    private void Start()
    {
        EventSystem.singleton.OnZoomCamera.AddListener(CameraZoom);
        _isZoom = false;

        if (isLocalPlayer)
            pos = transform;
    }

    [ClientRpc]
    private void CameraZoom(GameObject owner, GameObject target, bool typeZoom)
    {
        StartCoroutine(ZoomCamera(GetComponent<PlayerController>()._camera, typeZoom));
    }

    IEnumerator ZoomCamera(Camera camera, bool typeZoom)
    {
        if (_isZoom == true) yield break;

        yield return new WaitForSeconds(0.5f);

        _isZoom = true;

        float _currentTimeCurve = 0;
        float _totalTimeCurve = 0;
        bool action = true;

        if (typeZoom)
            _totalTimeCurve = _cameraZoomSetting.keys[_cameraZoomSetting.keys.Length - 1].time;
        else
            _currentTimeCurve = _cameraZoomSetting.keys[_cameraZoomSetting.keys.Length - 1].time;

        while (action)
        {
            try
            {
                camera.transform.position = new Vector3(pos.position.x, pos.position.y, _cameraZoomSetting.Evaluate(_currentTimeCurve));
            }
            catch (System.Exception)
            {
                break;
            }

            if (typeZoom)
            {
                _currentTimeCurve += Time.deltaTime;
                action = _totalTimeCurve >= _currentTimeCurve;
            }
            else
            {
                _currentTimeCurve -= Time.deltaTime;
                action = _totalTimeCurve <= _currentTimeCurve;
            }

            yield return null;
        }

        _isZoom = false;
        yield break;
    }




}
