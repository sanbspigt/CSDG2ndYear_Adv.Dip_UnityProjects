using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform previousParent;

    [HideInInspector]
    public int value;

    //[SerializeField]
    TextMeshProUGUI valueTextBox;
    Vector3 startPosition;
    void Awake()
    {
        valueTextBox = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        startPosition = transform.position;
    }
    public void SetValueToText(int value)
    {
        this.value = value;
        if(valueTextBox!=null)
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
