using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class EventSystem : NetworkBehaviour
{
    public static EventSystem singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

    public UnityEvent<GameObject, GameObject, bool> OnZoomCamera = new UnityEvent<GameObject, GameObject, bool>();

}
