using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;
    public bool isDragAndDroppable = true;
    protected Vector3 startDragPoint;

    protected Transform startParent;
    protected float DragDistance
    {
        get {return Mathf.Abs(Input.mousePosition.y - startDragPoint.y);}
    }
    private bool m_isDragging;
    public bool isDragging
    {
        get{return m_isDragging;}
        set
        {
            if(OnDragBoolChange != null)
                OnDragBoolChange(value);
        }
    }
    public delegate void OnDragAndDropDelegate(bool isDragging);
    public event OnDragAndDropDelegate OnDragBoolChange;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isDragAndDroppable)
        {
            startParent = transform.parent;
            isDragging = true;
            lastMousePosition = eventData.position;
            startDragPoint = eventData.position;

            OnStartDragAction();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDragAndDroppable)
        {
            Vector2 currentMousePosition = eventData.position;
            Vector2 diff = currentMousePosition - lastMousePosition;
            RectTransform rect = GetComponent<RectTransform>();
 
            Vector3 newPosition = rect.position +  new Vector3(diff.x, diff.y, transform.position.z);
            Vector3 oldPos = rect.position;
            rect.position = newPosition;

            lastMousePosition = currentMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        OnEndDragAction();
    }

    public virtual void OnEndDragAction()
    {
        Debug.Log("Cette méthode doit être override");
    }

    public virtual void OnStartDragAction()
    {
        Debug.Log("Cette méthode doit être override");
    }
}
