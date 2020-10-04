using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public ScriptableSection scriptSection;


    public Troncon tronconLeft;
    public Troncon tronconCenter;
    public Troncon tronconRight;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSection(ScriptableSection scriptToInitFrom)
    {
        scriptSection = scriptToInitFrom;
        name = scriptSection.name;
    }

    public void LoadObstacles()
    {
        Obstacle currentObstacle;

        //Obstacle gauche
        if(scriptSection.obstacleLeft)
        {
            Instantiate(ObstacleManager.Instance.GetObstacleFromScript(scriptSection.obstacleLeft), tronconLeft.transform).TryGetComponent(out currentObstacle);
            currentObstacle.scriptObstacle = scriptSection.obstacleLeft;
            currentObstacle.LoadScriptobstacle();
            tronconLeft.obstacle = currentObstacle;
        }

        //Obstacle centre
        if(scriptSection.obstacleCenter)
        {
            Instantiate(ObstacleManager.Instance.GetObstacleFromScript(scriptSection.obstacleCenter), tronconCenter.transform).TryGetComponent(out currentObstacle);
            currentObstacle.scriptObstacle = scriptSection.obstacleCenter;
            currentObstacle.LoadScriptobstacle();
            tronconCenter.obstacle = currentObstacle;
        }

        //Obstacle droit
        if (scriptSection.obstacleRight)
        {
            Instantiate(ObstacleManager.Instance.GetObstacleFromScript(scriptSection.obstacleRight), tronconRight.transform).TryGetComponent(out currentObstacle);
            currentObstacle.scriptObstacle = scriptSection.obstacleRight;
            currentObstacle.LoadScriptobstacle();
            tronconRight.obstacle = currentObstacle;
        }
    }

    public void AssignObstacleToScriptSection(ScriptableObstacle obstacleToAssigne, Troncon troncon)
    {
        if(troncon == tronconLeft)
        {
            scriptSection.obstacleLeft = obstacleToAssigne;
        }
        else if (troncon == tronconCenter)
        {
            scriptSection.obstacleCenter = obstacleToAssigne;
        }
        else
        {
            scriptSection.obstacleRight = obstacleToAssigne;
        }
    }
}
