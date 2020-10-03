using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgoAction : DragAndDrop
{
    public Image algoActionVisual;

    public Sprite algoActionImage_Up;

    public Sprite algoActionImage_Right;

    public Sprite algoActionImage_Left;

    public Sprite algoActionImage_Activate;

    public Sprite algoActionImage_Shoot;

    private InventaireHandler.AlgoActionEnum m_algoAction;
    public InventaireHandler.AlgoActionEnum AlgoActions
    {
        get {return m_algoAction;}
        set {
                m_algoAction = value;
                DisplayAlgoAction(value);
        }
    }

    private void DisplayAlgoAction(InventaireHandler.AlgoActionEnum algoAction)
    {
        switch(algoAction)
        {
            case InventaireHandler.AlgoActionEnum.Up :
                algoActionVisual.sprite = algoActionImage_Up;
            break;

            case InventaireHandler.AlgoActionEnum.Right :
                algoActionVisual.sprite = algoActionImage_Right;
            break;

            case InventaireHandler.AlgoActionEnum.Left :
                algoActionVisual.sprite = algoActionImage_Left;
            break;

            case InventaireHandler.AlgoActionEnum.Activate :
                algoActionVisual.sprite = algoActionImage_Activate;
            break;

            case InventaireHandler.AlgoActionEnum.Shoot :
                algoActionVisual.sprite = algoActionImage_Shoot;
            break;
        }
    }

    public override void OnEndDragAction()
    {
        base.OnEndDragAction();
        Debug.Log("On End Drag algo boxes");
    }
}
