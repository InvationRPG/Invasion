using UnityEngine;
using Mirror;


public class ItemController : NetworkBehaviour
{
    [SerializeField]
    private ItemData _itemData;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        InventoryController inv = collision.gameObject.GetComponent<InventoryController>();

        /*
          animation func
        */

        inv.AddItem(_itemData);

        NetworkServer.Destroy(gameObject);
    }

}
