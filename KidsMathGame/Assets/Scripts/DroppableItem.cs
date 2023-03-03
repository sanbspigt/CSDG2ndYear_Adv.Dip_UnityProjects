using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableItem : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Gameobject Name: "
                + eventData.pointerDrag.name
                +"  Dropable Obj: "+transform.gameObject.name);
        }
    }

    
}
