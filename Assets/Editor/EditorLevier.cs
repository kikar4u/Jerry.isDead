using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Levier))]
public class EditorLevier : Editor
{
    bool isAddDoorMode;
    Levier levier;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        levier = (Levier)target;

        GUILayout.Space(10);

        DoorMode();
    }

    private void OnSceneGUI()
    {
        if(isAddDoorMode)
        {
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                Porte porteSelected = null;

                if (gameObject.TryGetComponent(out porteSelected))
                {
                    levier.porte = porteSelected;
                    porteSelected.levier = levier;

                    ScriptableLevier scriptLevier = (ScriptableLevier)levier.scriptObstacle;
                    ScriptablePorte scriptPorte = (ScriptablePorte)porteSelected.scriptObstacle;

                    scriptLevier.scriptPorteToOpen = scriptPorte;
                    scriptPorte.scriptLevier = scriptLevier;

                    AssetDatabase.Refresh();
                    EditorUtility.SetDirty(scriptPorte);
                    EditorUtility.SetDirty(scriptLevier);
                    AssetDatabase.SaveAssets();


                    isAddDoorMode = false;
                    break;
                }
            }

            Selection.objects = new Object[1] { levier.gameObject };
        }
    }

    private void DoorMode()
    {
        if(isAddDoorMode)
        {
            if (GUILayout.Button("Annuler"))
            {
                isAddDoorMode = false;
            }
        }
        else
        {
            if (GUILayout.Button("Lier Porte"))
            {
                isAddDoorMode = true;
            }
        }
    }
}
