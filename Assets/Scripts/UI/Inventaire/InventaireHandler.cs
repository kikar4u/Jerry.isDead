using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaireHandler : MonoBehaviour
{
    public enum AlgoActionEnum {Up, Right, Left, Shoot, Activate};

    public Dictionary<AlgoActionEnum, int> playerActions;

    public GameObject actionContainerPrefab;

    public GameObject body;

    public GameObject playerActionPrefab;

    public List<AlgoActionEnum> algoActionsList;

    private bool m_isInit;
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
    }

    private void DisplayPlayerActions()
    {
        GameObject currentActionContainer = null;
        int currentActionContainerIndex = 0;
        int test = 0;
        foreach(KeyValuePair<AlgoActionEnum,int> playerAction in playerActions)
        {
            for(int i = 0, n = playerAction.Value; i < n; i++)
            {
                if(currentActionContainerIndex%3 == 0 || currentActionContainer == null)
                {
                    currentActionContainer = Instantiate(actionContainerPrefab, transform.position, Quaternion.identity);
                    currentActionContainer.transform.SetParent(body.transform);
                    currentActionContainer.name += i;
                    currentActionContainerIndex = currentActionContainerIndex;
                }

                if(currentActionContainer != null)
                {
                    GameObject playerActionGo = Instantiate(playerActionPrefab, transform.position, Quaternion.identity);
                    playerActionGo.transform.SetParent(currentActionContainer.transform);
                    playerActionGo.GetComponent<AlgoAction>().AlgoActions = playerAction.Key;
                    playerActionGo.GetComponent<AlgoAction>().isDragAndDroppable = true;
                }
                algoActionsList.Add(playerAction.Key);
                currentActionContainerIndex++;
            }
            test ++;
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
