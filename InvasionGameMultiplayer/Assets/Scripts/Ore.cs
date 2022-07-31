using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Ore : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    GameObject _player;

    [SerializeField]
    private int _id;
    [SerializeField]
    private string _description;
    [SerializeField]
    private int _digTimeSec;
    [SerializeField]
    private int _minLevelPick;

    public int Id => _id;
    public string Description => _description;
    public int DigTimeSec => _digTimeSec;
    public int MinLevelPick => _minLevelPick;

    public GameObject Player
    {
        set { _player = value; }
        get { return _player; }
    }

}



