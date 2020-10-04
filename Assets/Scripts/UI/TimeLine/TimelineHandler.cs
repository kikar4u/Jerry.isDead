using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimelineHandler : MonoBehaviour
{   
    private bool m_isInit;

    public float timelineDuration;

    public GameObject timeLineDotContainer;
    public GameObject dotPrefab;

    public GameObject scrollView;

    public Vector2 offsetSpawn;

    public List<GameObject> dotPrefabList;

    private bool isMouseOnTimeLine;

    private bool canStart;

    private float currentTime;

    public TextMeshProUGUI timeText;

    public bool IsMouseOnTimeLine{
        get{return isMouseOnTimeLine;}
        set{
            isMouseOnTimeLine = value;
        }
    }


    public void Open()
    {
        timelineDuration = GameManager.Instance.levelChucks;
        if(!m_isInit) Init();
        canStart = true;
    }

    public void Init()
    {
        m_isInit = true;
        SetupTimelineDots();
    }

    public void Close()
    {
        canStart = false;
    }

    public void Reset()
    {

    }

    private void Update() 
    {
        if(canStart)
        {
            currentTime += Time.deltaTime;
            timeText.text = Utility.TransformFloatToMMssmmTime(currentTime);
        }   
    }

    public void SetupTimelineDots()
    {
        for(int i = 0; i < timelineDuration; i++)
        {
            GameObject go = Instantiate(dotPrefab, transform.position, Quaternion.identity);
            go.GetComponent<RectTransform>().position = new Vector3(0f + offsetSpawn.x, i * go.GetComponent<RectTransform>().rect.height + offsetSpawn.y);
            go.transform.SetParent(timeLineDotContainer.transform);
            dotPrefabList.Add(go);
            go.GetComponent<TimelineDot>().number.text = (i).ToString();
        }

        scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(
            scrollView.GetComponent<RectTransform>().sizeDelta.x, 
            dotPrefabList.Count * dotPrefabList[0].GetComponent<RectTransform>().rect.height);
    }
}
