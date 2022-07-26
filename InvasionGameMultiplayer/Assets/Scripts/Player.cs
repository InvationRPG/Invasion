using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar] [SerializeField] private float _speed;

    [SyncVar(hook = nameof(SyncHealth))]
    int _SyncHealth;

    public int Health;
    public GameObject[] HealthGos;
    

    void Update()
    {
        if (isLocalPlayer)
        {
            MovePlayer();

            if (Input.GetKeyDown(KeyCode.H))
                ChangeHealth();
        }

        for (int i = 0; i < HealthGos.Length; i++)
            HealthGos[i].SetActive(!(Health - 1 < i));

    }


    #region MovePlayer

    private void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float _speed = 5f * Time.deltaTime;
        transform.Translate(new Vector2(h * _speed, v * _speed));
    }

    #endregion

    #region HealthPlayer

    private void ChangeHealth()
    {
        if (isServer)
            ChangeHealthValue(Health - 1);
        else
            CmdChangeHealth(Health - 1);
    }

    void SyncHealth(int oldValue, int newValue)
    {
        Health = newValue;
    }

    [Server]
    public void ChangeHealthValue(int newValue)
    {
        _SyncHealth = newValue;
    }

    [Command]
    public void CmdChangeHealth(int newValue)
    {
        ChangeHealthValue(newValue);
    }

    

    #endregion

}
