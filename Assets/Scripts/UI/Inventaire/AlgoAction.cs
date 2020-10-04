using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlgoAction : DragAndDrop
{
    private Action m_actionData;

    public Image algoActionImage;

    public void SetupAlgoAction(Action action)
    {
        m_actionData = action;
        isDragAndDroppable = true;
        isDragging = true;
        algoActionImage.sprite = action.actionActivated;
        SetVisibility(0.0f);
    }

    private void SetVisibility(float alpha)
    {
        Utility.ChangeAlpha(algoActionImage, alpha);
    }

    public override void OnStartDragAction()
    {
        base.OnStartDragAction();
        transform.SetParent(UIManager.Instance.inventaire.GetComponent<InventaireHandler>().tmpAlgoActionContainer.transform);
        SetVisibility(1.0f);
    }
}
