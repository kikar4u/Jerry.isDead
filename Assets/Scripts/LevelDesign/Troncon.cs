using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troncon : MonoBehaviour
{
    public Obstacle obstacle;
    public Section sectionParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearTronconFromObstacle()
    {
        if(obstacle)
        {
            if (Application.isPlaying)
                Destroy(obstacle.gameObject);
            else
                DestroyImmediate(obstacle.gameObject);
        }
    }
}
