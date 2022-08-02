using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Ore : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    GameObject _player;

    public GameObject Player
    {
        set { _player = value; }
        get { return _player; }
    }
}



