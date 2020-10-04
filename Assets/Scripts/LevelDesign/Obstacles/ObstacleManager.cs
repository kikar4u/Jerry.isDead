using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager thisOne;

    public static ObstacleManager Instance
    {
        get
        {
            if(thisOne == null)
            {
                thisOne = FindObjectOfType<ObstacleManager>();
            }
            return thisOne;
        }
    }

    public List<Obstacle> ListPrefabObstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Obstacle GetObstacleFromScript(ScriptableObstacle scriptObstacle)
    {
        foreach (Obstacle obstacle in ListPrefabObstacles)
        {
            if(scriptObstacle is ScriptableMur && obstacle.scriptObstacle is ScriptableMur)
            {
                return obstacle;
            }
            if (scriptObstacle is ScriptableCompresseur && obstacle.scriptObstacle is ScriptableCompresseur)
            {
                return obstacle;
            }
            if (scriptObstacle is ScriptableBroyeur && obstacle.scriptObstacle is ScriptableBroyeur)
            {
                return obstacle;
            }
            if (scriptObstacle is ScriptableLevier && obstacle.scriptObstacle is ScriptableLevier)
            {
                return obstacle;
            }
            if (scriptObstacle is ScriptablePorte && obstacle.scriptObstacle is ScriptablePorte)
            {
                return obstacle;
            }
            if (scriptObstacle is ScriptableTourelleAuto && obstacle.scriptObstacle is ScriptableTourelleAuto)
            {
                return obstacle;
            }

        }
        return null;
    }
}
