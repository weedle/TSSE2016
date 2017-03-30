using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class holder : MonoBehaviour, IDropHandler {
	
	void Start() {

	}

	public GameObject draggedObject {
		get {
			if (transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		Debug.Log ("onDrop is called");
		if (!draggedObject)
        {                               // check if slot has an item already
            GameObject obj = GameObject.Instantiate(dragHandler.draggedObject.gameObject);
            obj.GetComponent<IconHandler>().setNum(1);                              // check if slot has an item already
            obj.transform.SetParent(transform);
            dragHandler.draggedObject.GetComponent<IconHandler>()
                .decOne();
            ItemAbstract item = ItemDefinitions.stringToItem(
                draggedObject.GetComponent<ItemBasic>().itemString);
            transform.parent.GetComponent<ShipModSlotHandler>().setShipItem(item);
            //dragHandler.draggedObject.transform.SetParent(transform);

            print("obj: " + draggedObject.GetComponent<ItemBasic>().itemString);
            print("item: " + item.getType());
            ShipDefinitions.ShipEntity entity = ShipDefinitions.loadShip(transform.parent.GetComponent<ShipModSlotHandler>().thisEntity.uniqueId);
            print("ship: " + entity.uniqueId);
            print("currentItems: " + ItemDefinitions.itemsToString(entity.items));
            entity.items.Add(item);
            print("newCurrentItems: " + ItemDefinitions.itemsToString(entity.items));
            ShipDefinitions.saveShip(entity);
        }
    }
	#endregion
}
