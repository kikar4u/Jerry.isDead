using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject inventaire;

    public GameObject timeline;

    public GameObject pauseMenu;


    public void Init()
    {
        ShowHideInventaire();
        ShowHideTimeline();
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

    public void ShowHidePause(bool boo = true)
    {
        pauseMenu.SetActive(boo);
    }
}
