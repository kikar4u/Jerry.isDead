using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFin : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        SpaceWheel.Instance.eventSequenceEnds.AddListener(PlayerWin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerWin()
    {
        PlayerMov player;
        if(CheckForPlayer(out player))
        {
            GameManager.Instance.GameWin();
        }
    }

    public override void LoadScriptobstacle()
    {
        base.LoadScriptobstacle();

        ScriptableTriggerFin scriptTrigger = (ScriptableTriggerFin)scriptObstacle;
    }
}
