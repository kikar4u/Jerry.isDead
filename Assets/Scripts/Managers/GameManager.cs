using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject currentGameObjectDragged;
    [HideInInspector] public int levelChucks;

    void Start()
    {
        Init();
    }

    void Init()
    {
        SpaceWheel.Instance.Init();
        levelChucks = SpaceWheel.Instance.levelToLoad.listSections.Count;
        UIManager.Instance.Init();
    }

}
