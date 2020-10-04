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

        Vector3 direction = new Vector3();
        ScriptableTourelleAuto scriptTourelle = (ScriptableTourelleAuto)tourelle.scriptObstacle;

        if(GUILayout.Button("^\n||", optionsButtonArrowUpDown))
        {
            direction = Vector3.back;
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<==", optionsButtonArrowRightLeft))
        {
            direction = Vector3.right;
        }
        if (GUILayout.Button("==>", optionsButtonArrowRightLeft))
        {
            direction = Vector3.left;
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("||\nˇ",optionsButtonArrowUpDown))
        {
            direction = Vector3.forward;
        }

        tourelle.RotateHead(direction);
        scriptTourelle.directionTourelle = direction;
    }
}
