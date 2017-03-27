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
		if (!draggedObject) {								// check if slot has an item already
			dragHandler.draggedObject.transform.SetParent(transform);
		}
	}
	#endregion
}
