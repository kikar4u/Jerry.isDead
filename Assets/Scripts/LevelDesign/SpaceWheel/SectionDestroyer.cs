using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Section collidedSection;
        if (other.TryGetComponent(out collidedSection))
        {
            Destroy(collidedSection.gameObject);
        }   
    }

}


