using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public enum OreType
{
    Unknown,
    StoneOre,
    IronOre
}

public class Ore : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    GameObject _player;

    [SyncVar]
    [SerializeField]
    OreType _oreType;

    public GameObject Player
    {
        set { _player = value; }
        get { return _player; }
    }

    public virtual OreType GetOreType()
    {
        return _oreType;
    }

}



