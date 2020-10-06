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
                GameManager.Instance.GameOver();
            }
        }
        else
        {
           // Destroy(this); J'ai mis un destroy, parce que je suppose qu'IsOpen est la condition qui change suivant le levier
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
