using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    public float _speed;

    private void Start()
    {
        _speed = 5;
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x * _speed * Time.deltaTime, y * _speed * Time.deltaTime, 0);
        transform.position += move;

    }
}
