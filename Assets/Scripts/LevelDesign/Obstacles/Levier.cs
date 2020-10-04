using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Levier : Obstacle
{
    [HideInInspector] public UnityEvent eventLeverActivated = new UnityEvent();
    public Porte porte;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLever()
    {
        eventLeverActivated.Invoke();
    }

    public override void LoadScriptobstacle()
    {
        base.LoadScriptobstacle();

        ScriptableLevier scriptLevier = (ScriptableLevier)scriptObstacle;

        scriptLevier.levier = this;
    }
}
