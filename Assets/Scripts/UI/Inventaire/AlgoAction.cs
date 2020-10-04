using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlgoAction : DragAndDrop
{
    [HideInInspector]public Action m_actionData;

    public Image algoActionImage;

    public Image plugedImage;

    private RectTransform startState;

    [HideInInspector] public AlgoActionSpawner initialSpawner;

    public void SetupAlgoAction(Action action)
    {
        startState = GetComponent<RectTransform>();
        m_actionData = action;
        isDragAndDroppable = true;
        isDragging = true;
        algoActionImage.sprite = action.actionActivated;
        Utility.ChangeAlpha(algoActionImage, 0.0f);
    }

    private void SetVisibility(float alpha, Image target)
    {
        Utility.ChangeAlpha(target, alpha);
    }

    public override void OnStartDragAction()
    {
        transform.SetParent(UIManager.Instance.inventaire.GetComponent<InventaireHandler>().tmpAlgoActionContainer.transform);
        Utility.ChangeAlpha(algoActionImage, 1.0f);
        algoActionImage.raycastTarget = false;
        Utility.ChangeAlpha(plugedImage, 0.0f);
        plugedImage.raycastTarget = false;
        GameManager.Instance.currentGameObjectDragged = this.gameObject;

        if(startParent.GetComponentInParent<TimelineDot>())
        {
            startParent.GetComponentInParent<TimelineDot>().RemoveAlgoAction(this);
        }
    }

    public override void OnEndDragAction()
    {
        GameObject targetDot = UIManager.Instance.timeline.GetComponent<TimelineHandler>().timeLineDotContainer.GetComponent<DotsHandler>().LastMouseOverlap;
        if(targetDot != null)
        {
            targetDot.GetComponent<TimelineDot>().AddAlgoAction(this);
            SetPluged();
        }
        else
        {
            ReturnToStartState();
        }
        algoActionImage.raycastTarget = true;
        plugedImage.raycastTarget = true;
        GameManager.Instance.currentGameObjectDragged = null;
    }

    public void SetPluged()
    {
        Utility.ChangeAlpha(plugedImage, 1.0f);
    }

    private void ReturnToStartState()
    {
        initialSpawner.AmountOfSpawnableItem++;
        Destroy(this.gameObject);
    }
}
