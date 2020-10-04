using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlgoActionSpawner : MonoBehaviour //*** SPAWNER **//
{
    public Image algoActionVisual;

    public TextMeshProUGUI amountOfAction;

    public InventaireHandler.AlgoActionEnum algoAction;

    private Action m_actionData;

    public void SetupAlgoAction(Action action, int amountToCreate)
    {
        algoAction = action.actionName;
        amountOfAction.text = amountToCreate.ToString();
        algoActionVisual.sprite = action.actionActivated;
        action = m_actionData;
    }

}
