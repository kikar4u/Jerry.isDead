using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : Obstacle
{
    public Levier levier;
    
    private bool isOpen = false;

    private void Awake()
    {
        if (levier) levier.eventLeverActivated.AddListener(OpenDoor);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpaceWheel.Instance.eventSequenceEnds.AddListener(TorchPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TorchPlayer()
    {
        if(!isOpen)
        {
            PlayerMov player;
            if (CheckForPlayer(out player))
            {
                print("/////GameOver !\\\\\\");
            }
        }
    }

    

    public void OpenDoor()
    {
        isOpen = true;
    }

    public override void LoadScriptobstacle()
    {
        base.LoadScriptobstacle();

        ScriptablePorte scriptPorte = (ScriptablePorte)scriptObstacle;

        if(scriptPorte.scriptLevier)
        {
            levier = scriptPorte.scriptLevier.levier;
            levier.porte = this;
        }
    }
}
