using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventaireHandler : MonoBehaviour
{
    public enum AlgoActionEnum {Up, Right, Left, Shoot, Activate};

    public Dictionary<AlgoActionEnum, int> playerActions;

    public GameObject actionContainerPrefab;

    public GameObject bodyAction;

    public GameObject bodyDirection;

    public GameObject playerActionPrefab;

    public List<AlgoActionEnum> algoActionsList;
    private bool m_isInit;

    [HideInInspector]public List<Action> actionScriptableArray;    

    public void Open()
    {
        if(!m_isInit) Init();
        DisplayPlayerActions();
    }

    void Init()
    {
        m_isInit = true;
        playerActions = new Dictionary<AlgoActionEnum, int>();
        algoActionsList = new List<AlgoActionEnum>();

        /* pas eu le temps d'ajouter la serialisation des dictionnaires sur unity (go plugin) -> TODO*/
        playerActions.Add(AlgoActionEnum.Up,2);
        playerActions.Add(AlgoActionEnum.Right,1);
        playerActions.Add(AlgoActionEnum.Left,1);
        playerActions.Add(AlgoActionEnum.Activate,5);
        playerActions.Add(AlgoActionEnum.Shoot,2);

        actionScriptableArray = Resources.LoadAll<Action>("Action").ToList();
    }

    private void DisplayPlayerActions()
    {
        GameObject currentActionContainerBodyAction = null;
        int currentActionContainerIndexAction = 0;

        GameObject currentActionContainerBodyDirection = null;
        int currentActionContainerIndexDirection = 0;

        foreach(KeyValuePair<AlgoActionEnum,int> playerAction in playerActions)
        {
            Action targetAction = actionScriptableArray.Find(x => x.actionName == playerAction.Key);

            if(targetAction != null)
            {
                if(targetAction.actionType == Action.ActionType.Direction)
                {
                    if(currentActionContainerIndexDirection%2 == 0 || currentActionContainerBodyDirection == null)
                    {
                        currentActionContainerBodyDirection = Instantiate(actionContainerPrefab, transform.position, Quaternion.identity);
                        currentActionContainerBodyDirection.transform.SetParent(bodyDirection.transform);
                        currentActionContainerBodyDirection.name += currentActionContainerIndexDirection;
                    }

                    if(currentActionContainerBodyDirection != null)
                    {
                        GameObject playerActionGo = Instantiate(playerActionPrefab, transform.position, Quaternion.identity);
                        playerActionGo.transform.SetParent(currentActionContainerBodyDirection.transform);
                        playerActionGo.GetComponent<AlgoActionSpawner>().SetupAlgoAction(targetAction, playerAction.Value);
                    }
                    currentActionContainerIndexDirection++;               
                }
                else if (targetAction.actionType == Action.ActionType.Action)
                {
                    if(currentActionContainerIndexAction%2 == 0 || currentActionContainerBodyAction == null)
                    {
                        currentActionContainerBodyAction = Instantiate(actionContainerPrefab, transform.position, Quaternion.identity);
                        currentActionContainerBodyAction.transform.SetParent(bodyAction.transform);
                        currentActionContainerBodyAction.name += currentActionContainerIndexAction;
                    }
                        
                    if(currentActionContainerBodyAction != null)
                    {
                        GameObject playerActionGo = Instantiate(playerActionPrefab, transform.position, Quaternion.identity);
                        playerActionGo.transform.SetParent(currentActionContainerBodyAction.transform);
                        playerActionGo.GetComponent<AlgoActionSpawner>().SetupAlgoAction(targetAction, playerAction.Value);
                    }

                    currentActionContainerIndexAction++;
                 }

                algoActionsList.Add(playerAction.Key);
            }
        }
    }

    public void Close()
    {

    }

    void Reset()
    {
        m_isInit = false;
    }
}
