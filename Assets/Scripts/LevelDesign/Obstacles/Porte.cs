﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : Obstacle
{
    public Levier levier;
    private void Awake()
    {
        if (levier) levier.eventLeverActivated.AddListener(OpenDoor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        
    }

    public override void LoadScriptobstacle()
    {
        base.LoadScriptobstacle();

        ScriptablePorte scriptPorte = (ScriptablePorte)scriptObstacle;

        if(scriptPorte.scriptLevier) levier = scriptPorte.scriptLevier.levier;
    }
}