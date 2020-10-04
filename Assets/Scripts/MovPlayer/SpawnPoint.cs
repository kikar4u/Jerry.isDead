using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject m_plane;

    
    void Start()
    {
        Instantiate(m_plane, transform.position, Quaternion.identity);
    }

   
}
