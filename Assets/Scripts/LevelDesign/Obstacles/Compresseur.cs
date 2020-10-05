using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compresseur : Obstacle
{

    // Start is called before the first frame update
    void Start()
    {
        SpaceWheel.Instance.eventSequenceEnds.AddListener(CompressPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CompressPlayer()
    {
        PlayerMov player;
        if (CheckForPlayer(out player))
        {
            GameManager.Instance.GameOver();
        }
    }
}
