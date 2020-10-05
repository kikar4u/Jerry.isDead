using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isslowDown = true;
    public GameObject currentGameObjectDragged;
    [HideInInspector] public int levelChucks;

    public GameObject character;

    void Start()
    {
        if(isslowDown)
        Time.timeScale = 0.2f;
        Init();
    }

    void Init()
    {
        SpaceWheel.Instance.Init();
        levelChucks = SpaceWheel.Instance.levelToLoad.listSections.Count;
        UIManager.Instance.Init();
    }

    public void GameOver()
    {
        SpaceWheel.Instance.breakRotation = true;

        //Faudra peut-être attendre que l'annim' de mort se termine.

        RestartLevel();
    }

    private void RestartLevel()
    {
        //Player Init()
        SpaceWheel.Instance.Init();
    }

    public void GameWin()
    {
        //Ouvrir l'écran de victoire

        PushNextLevel(); //Peut-être mettre cette fonction dans un bouton "nivo suivant"
    }

    private void PushNextLevel()
    {
        SpaceWheel wheel = SpaceWheel.Instance;


        if (wheel.indexCampagne < wheel.campagneToLoad.listLevels.Count - 1)
        {
            wheel.indexCampagne++;

            //Player Init()
            wheel.Init();
        }
        else
        {
            //finir campagne/Ecran de fin avec remerciements toussa   OU   Prochain campagne
            print("Jeu fini");
        }

    }

}
