using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject inventaire;


    public void Init()
    {
        ShowHideInventaire();
    }

    public void ShowHideInventaire(bool boo = true)
    {
        if(boo) inventaire.GetComponent<InventaireHandler>().Open();
        else inventaire.GetComponent<InventaireHandler>().Close();
    }
}
