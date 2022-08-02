using UnityEngine;
using Mirror;

public class PlayerMaining : NetworkBehaviour
{
    private GameObject _ore;

    private int _oreIndex;


    [SerializeField]
    private GameObject _pick;

    private int _pickIndex;

    private int _pickSpeed;

    private int _pickLevel;


    [SerializeField]
    private GameObject _prefab;

    private GameObject _currentMainingMessage;

    private GameObject _child;

    private bool _prefabMove;


    private float _timer;

    private float _section;


    [SerializeField]
    private OreData[] _ores;

    [SerializeField]
    private PickData[] _picks;

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
        _ore = other.gameObject;
        Ore OreComponent = _ore.GetComponent<Ore>();

        SelectIndexOre();

        if (_oreIndex == -1)
            return;

        if (OreComponent.Player != null)
            return;

        SelectPick();

        OreComponent.Player = gameObject;
        InstantiateMessageMaining();
        _prefabMove = true;
        
    }

    [ServerCallback]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_ore == null)
            return;

        DestroyMessageMaining();

        _ore.GetComponent<Ore>().Player = null;
        _prefabMove = false;
        _ore = null;
    }   


    [Server]
    private void SelectPick()
    {
        if (_pick != null)
        {
            SelectIndexPick();
        }
        else
        {
            _pickSpeed = 0;
            _pickLevel = 0;
        }
    }

    [Server]
    private void SelectIndexOre()
    {
        if (_ore.tag == "Stone")
            _oreIndex = 0;
        else if (_ore.tag == "Iron")
            _oreIndex = 1;
        else
            _oreIndex = -1;
    }

    [Server]
    private void SelectIndexPick()
    {
        if (_pick.GetComponent<Pick>().Name == "StonePick")
            _pickIndex = 0;

        _pickSpeed = _picks[_pickIndex].Speed;
        _pickLevel = _picks[_pickIndex].Level;

    }


    [TargetRpc]
    private void InstantiateMessageMaining()
    {
        Instantiate(_prefab);
        _currentMainingMessage = GameObject.FindGameObjectWithTag("MainingMessage");
        _child = _currentMainingMessage.transform.GetChild(1).gameObject;
    }

    [TargetRpc]
    private void DestroyMessageMaining()
    {
        Destroy(_currentMainingMessage);
        _currentMainingMessage = null;
        _child = null;
        _timer = 0;
        _section = 0;
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

        if (_pickLevel < _ores[_oreIndex].MinLevelPick)
        {
            _currentMainingMessage.transform.GetChild(0).gameObject.SetActive(false);
            _currentMainingMessage.transform.GetChild(1).gameObject.SetActive(false);
            _currentMainingMessage.transform.GetChild(2).gameObject.SetActive(true);
            return;
        }

        _currentMainingMessage.transform.GetChild(2).gameObject.SetActive(false);


        if (Input.GetKey(KeyCode.X))
        {

            _section = (_timer - 0) / (_ores[_oreIndex].DigTimeSec - _pickSpeed) * 1;
            _currentMainingMessage.transform.GetChild(0).gameObject.SetActive(false);
            _child.SetActive(true);
            _child.transform.localScale = new Vector3(_section, 0.3f);
            _child.transform.position = new Vector3(_currentMainingMessage.transform.position.x - 0.25f + _section / 4, _currentMainingMessage.transform.position.y);

            _timer += Time.deltaTime;

            if (_timer >= _ores[_oreIndex].DigTimeSec - _pickSpeed)
                CmdMaininigOre();
        }
        else
        {
            _child.transform.position = new Vector3(_currentMainingMessage.transform.position.x - 0.25f, _currentMainingMessage.transform.position.y);
            _currentMainingMessage.transform.GetChild(0).gameObject.SetActive(true);
            _currentMainingMessage.transform.GetChild(1).gameObject.SetActive(false);
            _section = 0;
            _timer = 0;
        }
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
        _pickIndex = -1;
        _oreIndex = -1;
        _pickSpeed = 0;
        _ore = null;
    }


}
