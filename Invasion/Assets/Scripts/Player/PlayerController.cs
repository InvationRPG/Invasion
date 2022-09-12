using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    public float _speed;
    public Animator _animator;

    private void Start()
    {
        _speed = 10;
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isLocalPlayer) return;

        float x = Input.GetAxis("Horizontal") * _speed;
        float y = Input.GetAxis("Vertical") * _speed;

        _animator.SetFloat("SpeedY", y);
        _animator.SetFloat("SpeedX", x);

        Vector3 move = new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0);
        transform.position += move;
    }
}
