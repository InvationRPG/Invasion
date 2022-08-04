using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System;

public enum Typeobjects
{
    ingot,
    pick
}

public class InventoryController : NetworkBehaviour
{
    public Action<GameObject> onInventoryClose;
    [SerializeField] public  List<ItemData> _inventoryItems = new List<ItemData>();
    [SerializeField] private GameObject _inventoryPrefab;
    private bool _openInventory;
    private GameObject inventory;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.I))
            CmdOpenInventory();
        if (Input.GetKey(KeyCode.Escape))
            CmdCloseInventory();
    }

    [Server]
    public void AddItem(ItemData item)
    {
        _inventoryItems.Add(item);
    }

    [Command]
    private void CmdOpenInventory()
    {
        RpcOpenInventory();
    }

    [TargetRpc]
    private void RpcOpenInventory()
    {
        if (_openInventory)
            return;

        _openInventory = true;

        inventory = Instantiate(_inventoryPrefab);
        inventory.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    [Command]
    private void CmdCloseInventory()
    {
        RpcCloseInventory();
    }

    [TargetRpc]
    private void RpcCloseInventory()
    {
        if (!_openInventory)
            return;

        onInventoryClose?.Invoke(inventory);
        _openInventory = false;
        Destroy(inventory);
    }

}
