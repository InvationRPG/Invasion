using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server Started!");
    }

    public override void OnStopServer()
    {
        base.OnStartServer();
        Debug.Log("Server Stopped!");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log($"User: {conn.connectionId} connected!");
    }


    [System.Obsolete]
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("User: " + conn.connectionId + " disconnected.");
    }

}
