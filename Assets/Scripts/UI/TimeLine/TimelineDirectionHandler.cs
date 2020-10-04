using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineDirectionHandler : MonoBehaviour
{   
    private bool m_isInit;

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
