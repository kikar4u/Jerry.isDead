using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject inventaire;

    public GameObject timeline;


    public void Init()
    {
        ShowHideInventaire();
    }

    public void ShowHideInventaire(bool boo = true)
    {
        if(boo) inventaire.GetComponent<InventaireHandler>().Open();
        else inventaire.GetComponent<InventaireHandler>().Close();
    }

    public void ShowHideTimeline(bool boo = true)
    {
        if(boo) timeline.GetComponent<TimelineHandler>().Open();
        else timeline.GetComponent<TimelineHandler>().Close();
    }
}
