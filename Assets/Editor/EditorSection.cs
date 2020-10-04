using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Section), true)]
public class EditorSection : Editor
{
    Section section;
    ScriptableSection scriptSection;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        section = (Section)target;
        scriptSection = section.scriptSection;

        GUILayout.Space(20);

        DrawObstacleInterface();
    }

    private void SaveScriptSection()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(scriptSection);
        AssetDatabase.SaveAssets();
    }

    private void DrawObstacleInterface()
    {
        string[] listChoicesObstacles = CreateListNameObstacles();
        string nomObstacle;

        if(section.scriptSection)section.name = section.scriptSection.name;

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        if (GUILayout.Button("obstacle\ngauche"))
        {
            ObstacleSelectionWindow.OpenObstacleSelectionWindow(section.tronconLeft);
        }
        nomObstacle = section.tronconLeft.obstacle == null ? "Aucun" : section.tronconLeft.obstacle.name;

        GUILayout.Label(nomObstacle);
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        if (GUILayout.Button("obstacle\ncentre"))
        {
            ObstacleSelectionWindow.OpenObstacleSelectionWindow(section.tronconCenter);
        }
        nomObstacle = section.tronconCenter.obstacle == null ? "Aucun" : section.tronconCenter.obstacle.name;

        GUILayout.Label(nomObstacle);
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        if (GUILayout.Button("obstacle\ndroite"))
        {
            ObstacleSelectionWindow.OpenObstacleSelectionWindow(section.tronconRight);
        }
        nomObstacle = section.tronconRight.obstacle == null ? "Aucun" : section.tronconRight.obstacle.name;

        GUILayout.Label(nomObstacle);
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    private string[] CreateListNameObstacles()
    {
        string[] listChoicesObstacles = new string[ObstacleManager.Instance.ListPrefabObstacles.Count + 1];

        for (int i = 0; i < ObstacleManager.Instance.ListPrefabObstacles.Count; i++)
        {
            listChoicesObstacles[i] = ObstacleManager.Instance.ListPrefabObstacles[i].name;
        }

        listChoicesObstacles[listChoicesObstacles.Length - 1] = "Aucun";

        return listChoicesObstacles;
    }
}
