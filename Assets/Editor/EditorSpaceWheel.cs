using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpaceWheel))]
public class EditorSpaceWheel : Editor
{
    SpaceWheel wheel;
    int indexLevelToLoad = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        wheel = (SpaceWheel)target;

        GUILayout.Space(15);

        //if(LevelManager.Instance.listLevels.Count > 0)
        //{
        //    GUILayout.Label("Nivo à charger");
        //    indexLevelToLoad = EditorGUILayout.Popup(indexLevelToLoad, GenListNameLevel());

        //    wheel.levelToLoad = LevelManager.Instance.listLevels[indexLevelToLoad];
        //}
        //else
        //{
        //    GUILayout.Label("Aucun Nivo à charger");
        //}

        GUILayout.Space(10);

        SaveButton();
    }

    private string[] GenListNameLevel()
    {
        string[]  listNameLevel = new string[LevelManager.Instance.listLevels.Count];
            for (int i = 0; i < LevelManager.Instance.listLevels.Count; i++)
            {
                listNameLevel[i] = LevelManager.Instance.listLevels[i].name;
            }


        return listNameLevel;
    }

    private void SaveButton()
    {
        if(GUILayout.Button("Save"))
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(wheel);
            AssetDatabase.SaveAssets();
        }
    }
}
