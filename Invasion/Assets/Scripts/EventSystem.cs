using UnityEngine;
using Mirror;

public class EventSystem : NetworkBehaviour
{
    public static EventSystem singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }
}
