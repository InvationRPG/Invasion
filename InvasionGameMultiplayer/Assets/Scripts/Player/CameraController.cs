using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    [SerializeField]
    private GameObject _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        MoveCamera();
    }
   
    private void MoveCamera()
    {
        _camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }

}
