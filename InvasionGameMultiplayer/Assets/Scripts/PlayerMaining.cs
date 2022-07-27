using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMaining : NetworkBehaviour
{
    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    GameObject _ore;

    GameObject _currentMainingMessage;

    [SerializeField]
    bool _prefabMove;

    float _value;
    float _section;


    private void Update()
    {
        if (_prefabMove)
        {
            MovementMessageMaining();
            Maining();
        }
    }

    [ServerCallback]
    private void OnTriggerStay2D(Collider2D other)
    {

        GameObject obj = _ore = other.gameObject;
        Ore OreComponent = obj.GetComponent<Ore>();
        if (obj.tag == "Ore")
        {
            if (OreComponent.Player != null)
                return;

            OreComponent.Player = gameObject;
            InstantiateMessageMaining();
            _prefabMove = true;
        }
    }

    [ServerCallback]
    private void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            GameObject obj = other.gameObject;
            if (obj.tag == "Ore" && obj.GetComponent<Ore>().Player.GetComponent<Player>().netId == netId)
            {
                obj.GetComponent<Ore>().Player = null;
                _prefabMove = false;
                DestroyMessageMaining();
            }
        }
        catch
        {
            return;
        }
        
    }   


    [TargetRpc]
    private void InstantiateMessageMaining()
    {
        Instantiate(_prefab);
        _currentMainingMessage = GameObject.FindGameObjectWithTag("MainingMessage");
    }

    [TargetRpc]
    private void DestroyMessageMaining()
    {
        Destroy(_currentMainingMessage);
    }

    [TargetRpc]
    private void MovementMessageMaining()
    {
        Vector3 TransformPrefab = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2);
        _currentMainingMessage.transform.position = TransformPrefab;
    }

    [TargetRpc]
    private void Maining()
    {
        GameObject child = _currentMainingMessage.transform.GetChild(1).gameObject;

        if (Input.GetKey(KeyCode.X))
        {
            _section = (_value - 0) / 100 * 1;
            _currentMainingMessage.transform.GetChild(0).gameObject.SetActive(false);
            child.SetActive(true);
            child.transform.localScale = new Vector3(_section, 0.3f);
            child.transform.position = new Vector3(_currentMainingMessage.transform.position.x - 0.25f + _section / 4, _currentMainingMessage.transform.position.y);

            if (_value < 100)
                _value += 0.5f;

        }
        else
        {
            child.transform.position = new Vector3(_currentMainingMessage.transform.position.x - 0.25f, _currentMainingMessage.transform.position.y);
            _currentMainingMessage.transform.GetChild(0).gameObject.SetActive(true);
            _currentMainingMessage.transform.GetChild(1).gameObject.SetActive(false);
            _value = 0;
            _section = 0;
        }

        if (_value == 100)
            CmdMaininigOre();

    }

    [Command]
    private void CmdMaininigOre()
    {
        MaininigOre();
    }


    [Server]
    private void MaininigOre()
    {
        NetworkServer.Destroy(_ore);
        _ore = null;
    }

}
