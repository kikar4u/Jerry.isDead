using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DotsHandler : MonoBehaviour, IPointerExitHandler
{

    public GameObject lastMouseOverlap;

    public GameObject LastMouseOverlap
    {
        get{return lastMouseOverlap;}
        set{
            if(lastMouseOverlap != null)
            {
                TimelineDot prev_td = lastMouseOverlap.GetComponent<TimelineDot>();
                if(prev_td != null)
                {
                    prev_td.dotAction.color = Color.white;
                    prev_td.dotDirection.color = Color.white;
                }
            }

            lastMouseOverlap = value;
            TimelineDot td = value?.GetComponent<TimelineDot>();
            if(td != null)
            {
                td.dotAction.color = Color.red;
                td.dotDirection.color = Color.red;
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GetComponentInParent<DotsHandler>().LastMouseOverlap = null;
    }
}
