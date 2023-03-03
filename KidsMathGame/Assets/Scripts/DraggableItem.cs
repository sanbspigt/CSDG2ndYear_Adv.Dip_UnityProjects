using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform previousParent;

    [HideInInspector]
    public int value;

    [SerializeField]
    TextMeshProUGUI valueTextBox;
    
    public void SetValueToText(int value)
    {
        this.value = value;
        valueTextBox.text = value.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Beginning of Drag!");
        previousParent = transform.parent;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
        transform.parent = transform.root;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End of Drag!");
        transform.parent = previousParent;
        GetComponent<Image>().raycastTarget = true;
    }
}
