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
    }
}
