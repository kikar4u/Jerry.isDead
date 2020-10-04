using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineHandler : MonoBehaviour
{   
    private bool m_isInit;

    private bool isMouseOnTimeLine;

    public bool IsMouseOnTimeLine{
        get{return isMouseOnTimeLine;}
        set{
            isMouseOnTimeLine = value;
        }
    }


    public void Open()
    {
        if(!m_isInit) Init();
    }

    public void Init()
    {
        m_isInit = true;
    }

    public void Close()
    {

    }

    public void Reset()
    {

    }
}
