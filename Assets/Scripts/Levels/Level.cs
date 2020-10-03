using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour
{
    public List<Section> listSections = new List<Section>();
    private float depthSection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSectionFlat(Section sectionToAdd)
    {
        if(listSections.Count == 0)
        {
            depthSection = sectionToAdd.GetComponent<BoxCollider>().size.z;
        }

        listSections.Add(sectionToAdd);

        sectionToAdd.transform.localPosition = new Vector3(0, 0, (listSections.Count -1) * depthSection);
    }
}
