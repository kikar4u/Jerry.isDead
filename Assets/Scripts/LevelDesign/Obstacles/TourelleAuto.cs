using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleAuto : Obstacle
{
    public GameObject teteTourelle;

    // Start is called before the first frame update
    void Start()
    {
        SpaceWheel.Instance.eventSequenceEnds.AddListener(ShootPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootPlayer()
    {
        PlayerMov player;
        if (CheckForPlayer(out player))
        {
            print("/////GameOver !\\\\\\");
        }
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
        if (Application.isPlaying)
        {
            teteTourelle.transform.Rotate(new Vector3(0, 90, 90));
        }
        
        Debug.DrawLine(teteTourelle.transform.position, direction + teteTourelle.transform.position, new Color(200, 0, 0),20);
    }
}
