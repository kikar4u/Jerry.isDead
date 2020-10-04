using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleAuto : Obstacle
{
    public GameObject teteTourelle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void LoadScriptobstacle()
    {
        base.LoadScriptobstacle();

        ScriptableTourelleAuto scriptTourelle = (ScriptableTourelleAuto)scriptObstacle;

        RotateHead(scriptTourelle.directionTourelle);
    }

    public void RotateHead(Vector3 direction)
    {
        teteTourelle.transform.LookAt(direction + teteTourelle.transform.position);
    }
}
