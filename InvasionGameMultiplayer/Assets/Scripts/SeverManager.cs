using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SeverManager : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("Server Started!");
    }

    public override void OnStopServer()
    {
        Debug.Log("Server Stopped!");
    }

    [System.Obsolete]
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("User: " + conn.connectionId + " connected.");
    }

    [System.Obsolete]
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("User: " + conn.connectionId + " disconnected.");
    }

}
