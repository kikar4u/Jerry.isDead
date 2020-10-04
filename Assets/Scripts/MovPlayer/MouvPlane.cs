using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvPlane : MonoBehaviour
{
    public int m_Speed;


 
    void Update()
    {
        MoveDecore();
    }

    public void MoveDecore()
    {

       //transform.Translate(0, 0, 1  * m_Speed * Time.deltaTime);
       transform.Translate(Vector3.back * m_Speed * Time.deltaTime);

    }


}
