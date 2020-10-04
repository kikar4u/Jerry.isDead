using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject currentGameObjectDragged;
    [HideInInspector] public int levelChucks;

    public GameObject character;

    void Start()
    {
        //Time.timeScale = 0.2f;
        Init();
    }

    void Init()
    {
        SpaceWheel.Instance.Init();
        levelChucks = SpaceWheel.Instance.levelToLoad.listSections.Count;
        UIManager.Instance.Init();
    }

}
