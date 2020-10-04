using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TimelineDot : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI number;

    public Image dotDirection;

    public Image dotAction;

    public bool directionUsed;

    public bool actionUsed;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GetComponentInParent<DotsHandler>().LastMouseOverlap = this.gameObject;
    }
    
    public void AddAlgoAction(AlgoAction algoAction)
    {
        if(algoAction.m_actionData.actionType == Action.ActionType.Direction)
        {
            if(!directionUsed)
            {
                directionUsed = true;
                algoAction.gameObject.transform.SetParent(dotDirection.transform);
                algoAction.GetComponent<RectTransform>().transform.position = dotDirection.GetComponent<RectTransform>().transform.position;
                algoAction.transform.localScale = new Vector3(0.3f,0.3f);
            }
        }
        else if(algoAction.m_actionData.actionType == Action.ActionType.Action)
        {
            if(!actionUsed)
            {
                actionUsed = true;
                algoAction.gameObject.transform.SetParent(dotAction.transform);
                algoAction.GetComponent<RectTransform>().transform.position = dotAction.GetComponent<RectTransform>().transform.position;
                algoAction.transform.localScale = new Vector3(0.3f,0.3f);
            }
        }
    }

    public void RemoveAlgoAction(AlgoAction algoAction)
    {
        if(algoAction.m_actionData.actionType == Action.ActionType.Direction)
        {
            directionUsed = false;
        }
        else if(algoAction.m_actionData.actionType == Action.ActionType.Action)
        {
            actionUsed = false;
        }
    }
}
