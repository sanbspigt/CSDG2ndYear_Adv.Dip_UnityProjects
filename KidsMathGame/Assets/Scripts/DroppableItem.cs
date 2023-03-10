using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableItem : MonoBehaviour,IDropHandler
{
    GameManager GM;
    void Awake()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Gameobject Name: "
                + eventData.pointerDrag.name
                +"  Dropable Obj: "+transform.gameObject.name);

            eventData.pointerDrag.gameObject.
                GetComponent<DraggableItem>().previousParent = transform;

            eventData.pointerDrag.gameObject.
                transform.position = transform.position;

            DraggableItem DragItem = eventData.pointerDrag.gameObject.
                GetComponent<DraggableItem>();

            if (transform.GetSiblingIndex() == 0)
                GM.finalV1 = DragItem.value;
            else if(transform.GetSiblingIndex() == 1)
                GM.finalV2 = DragItem.value;
            else if (transform.GetSiblingIndex() == 2)
                GM.finalResult = DragItem.value;

            GM.CheckForLevelComplete();
            GM.CheckForLevelFail();
        }
    }

    
}
