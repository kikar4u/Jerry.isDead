using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TourelleAuto))]
public class TourelleEditor : Editor
{
    TourelleAuto tourelle;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        tourelle = (TourelleAuto)target;

        GUILayout.Space(20);

        DrawDirectionnalArrow();
    }

    private void DrawDirectionnalArrow()
    {
        GUILayoutOption[] optionsButtonArrowUpDown = new GUILayoutOption[2] {GUILayout.Height(50), GUILayout.Width(20)};
        GUILayoutOption[] optionsButtonArrowRightLeft = new GUILayoutOption[2] { GUILayout.Height(20), GUILayout.Width(50) };

        GUILayout.Label("Direction du tire");

        GUILayout.Space(10);

        if(GUILayout.Button("^\n||", optionsButtonArrowUpDown))
        {
            tourelle.teteTourelle.transform.LookAt(Vector3.back + tourelle.teteTourelle.transform.position);
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<==", optionsButtonArrowRightLeft))
        {
            tourelle.teteTourelle.transform.LookAt(Vector3.right + tourelle.teteTourelle.transform.position);
        }
        if (GUILayout.Button("==>", optionsButtonArrowRightLeft))
        {
            tourelle.teteTourelle.transform.LookAt(Vector3.left + tourelle.teteTourelle.transform.position);
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("||\nˇ",optionsButtonArrowUpDown))
        {
            tourelle.teteTourelle.transform.LookAt(Vector3.forward + tourelle.teteTourelle.transform.position);
        }
    }
}
