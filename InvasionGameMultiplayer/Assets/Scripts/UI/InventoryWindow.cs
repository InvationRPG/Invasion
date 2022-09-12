using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Mirror;

public class InventoryWindow : NetworkBehaviour
{
    [SerializeField] InventoryController _inventoryController;
    [SerializeField] RectTransform _itemsPanel;
    List<GameObject> _drawIcons = new List<GameObject>();

    private void Awake()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _inventoryController = _player.GetComponent<InventoryController>();
        _itemsPanel = transform.GetChild(0).GetComponent<RectTransform>();
        _inventoryController.onInventoryClose += OnInventoryClose;
        Redraw();
    }

    public void OnInventoryClose(GameObject obj) => ClearDrawn();

    [Server]
    private void Redraw()
    {
        ClearDrawn();
        foreach (var item in _inventoryController._inventoryItems)
        {
            var icon = new GameObject(item.Name);
            icon.AddComponent<Image>().sprite = item.Icon;
            icon.transform.SetParent(_itemsPanel);
            _drawIcons.Add(icon);
        }
    }

    private void ClearDrawn()
    {
        foreach (var item in _drawIcons)
            Destroy(item);

        _drawIcons.Clear();
    }

}
