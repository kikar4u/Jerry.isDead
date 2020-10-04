using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AlgoActionSpawner : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler 
{
    public Image algoActionVisual;

    public TextMeshProUGUI amountOfAction;

    public InventaireHandler.AlgoActionEnum algoAction;

    private Action m_actionData;

    private int amountOfSpawnableItem;

    public int AmountOfSpawnableItem{
        get {return amountOfSpawnableItem;}
        set {
            amountOfSpawnableItem = value;
            amountOfAction.text = value.ToString();
        }
    }

    public GameObject algoActionPrefab;

    private GameObject currentAlgoActionSpawned;

    public void SetupAlgoAction(Action action, int amountToCreate)
    {
        algoAction = action.actionName;
        algoActionVisual.sprite = action.actionActivated;
        m_actionData = action;
        AmountOfSpawnableItem = amountToCreate;
        currentAlgoActionSpawned = null;

        Spawn();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.2f * 0.4f;

        Spawn();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 0.4f;
    }

    private void Spawn()
    {
        if(currentAlgoActionSpawned == null && AmountOfSpawnableItem > 0)
        {
            GameObject go = Instantiate(algoActionPrefab, transform.position, Quaternion.identity);
            go.GetComponent<AlgoAction>().SetupAlgoAction(m_actionData);
            go.transform.SetParent(transform);
            currentAlgoActionSpawned = go;

            currentAlgoActionSpawned.GetComponent<DragAndDrop>().OnDragBoolChange += ChildGotDragged;
        }
    }

    void ChildGotDragged(bool isDragged)
    {
        if(isDragged)
        {
            AmountOfSpawnableItem--;
            currentAlgoActionSpawned = null;
        }
    }
}
