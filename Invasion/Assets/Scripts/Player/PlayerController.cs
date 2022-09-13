using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private float _speed;
    private Animator _animator;

    public Camera _camera;

    private void Start()
    {
        _speed = 10;
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
    }
    private void Update()
    {
        if (!isLocalPlayer) return;

        CameraMove();
        PlayerMove();
    }

    private void CameraMove()
    {
        _camera.transform.position = new Vector3(transform.position.x, transform.position.y, _camera.transform.position.z);
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal") * _speed;
        float y = Input.GetAxis("Vertical") * _speed;

        _animator.SetFloat("SpeedY", y);
        _animator.SetFloat("SpeedX", x);

        Vector3 move = new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0);
        transform.position += move;
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        EventSystem.singleton.OnZoomCamera.Invoke(gameObject, collision.gameObject, true);
        EventSystem.singleton.UserInteraction.Invoke(gameObject, true);
    }

    [ServerCallback]
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        EventSystem.singleton.OnZoomCamera.Invoke(gameObject, collision.gameObject, false);
        EventSystem.singleton.UserInteraction.Invoke(gameObject, false);

    }

}
