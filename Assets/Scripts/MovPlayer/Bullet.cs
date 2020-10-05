using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject prefabSectionDefaut;
    private float DetectionWidth
    {
        get
        {
            return 0;
        }
    }

    private Troncon nextDirection;
    [SerializeField] private int rangeInSection;
    [Tooltip("secondes nécessaire pour atteindre la prochaine section")]
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Troncon SeekForNextTroncon()
    {
        return null;
    }


}
