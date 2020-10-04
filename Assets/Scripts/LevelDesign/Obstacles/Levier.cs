using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Levier : Obstacle
{
    private UnityEvent eventLeverActivated = new UnityEvent();
    public Porte porte;

    private void Awake()
    {
        if(porte) eventLeverActivated.AddListener(porte.OpenDoor);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLever()
    {
        eventLeverActivated.Invoke();
    }
}
