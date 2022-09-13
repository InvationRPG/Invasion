using UnityEngine;
using Mirror;
using System.Collections;

public class UIPlayerController : NetworkBehaviour
{

    [SerializeField] private GameObject _slideButton;
    [SerializeField] private AnimationCurve _slideButtonAnimation;
    private bool _isSlide;

    private void Start()
    {
        EventSystem.singleton.UserInteraction.AddListener(UserInteraction);
    }

    [ClientRpc]
    private void UserInteraction(GameObject user, bool action)
    {
        if (!isLocalPlayer) return;

        StartCoroutine(Slide(action));
    }

    IEnumerator Slide(bool action)
    {
        if (_isSlide == true) yield break;

        yield return new WaitForSeconds(0.5f);

        _isSlide = true;

        bool working = true;
        float _currentTimeCurve = 0;
        float _totalTimeCurve = 0;

        if (action)
            _totalTimeCurve = _slideButtonAnimation.keys[_slideButtonAnimation.keys.Length - 1].time;
        else
            _currentTimeCurve = _slideButtonAnimation.keys[_slideButtonAnimation.keys.Length - 1].time;

        while (working)
        {
            _slideButton.transform.position = new Vector3(
                _slideButton.transform.position.x,
                _slideButtonAnimation.Evaluate(_currentTimeCurve),
                _slideButton.transform.position.z
                );


            if (action)
            {
                _currentTimeCurve += Time.deltaTime;
                working = _totalTimeCurve >= _currentTimeCurve;
            }
            else
            {
                _currentTimeCurve -= Time.deltaTime;
                working = _totalTimeCurve <= _currentTimeCurve;
            }

            yield return null;
        }

        _isSlide = false;
        yield break;
    }


}
