using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public ScriptableObstacle scriptObstacle;
    [SerializeField] protected Collider trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void LoadScriptobstacle()
    {

    }

    protected bool CheckForPlayer(out PlayerMov playerCollided)
    {

        Collider[] check = Physics.OverlapBox(transform.position, trigger.bounds.extents);

        foreach (Collider col in check)
        {
            PlayerMov player;
            if (col.TryGetComponent(out player))
            {
                playerCollided = player;
                return true;
            }
        }

        playerCollided = null;
        return false;
    }
}
