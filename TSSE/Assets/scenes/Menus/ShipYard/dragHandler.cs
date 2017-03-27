using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// this will be attached to the item that is BEING DRAGGED
// NOTES:
// 	- did not use GetComponent<CanvasGroup> ().blocksRaycasts = false / true;
public class dragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject draggedObject;
	Vector3 startPos;
	private Vector3 screenPoint;
	Transform startParent;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{	
		Debug.Log ("OnBeginDrag is called!");
		draggedObject = gameObject;
		startPos = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	#endregion


	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		screenPoint = Input.mousePosition;
		screenPoint.z = 1f;
		transform.position = Camera.main.ScreenToWorldPoint (screenPoint);

	}

	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		draggedObject = null;
	
		if (transform.parent == startParent) {
			transform.position = startPos;
		}

		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion
}