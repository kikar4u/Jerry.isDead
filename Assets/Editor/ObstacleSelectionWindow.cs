using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstacleSelectionWindow : EditorWindow
{
    GUILayoutOption[] optionsButtonObstacle = new GUILayoutOption[2] { GUILayout.Height(80), GUILayout.Width(80)};

    int colonnes = 4;

    Troncon troncon;

    public static void OpenObstacleSelectionWindow(Troncon tronconToApply)
    {
        ObstacleSelectionWindow thisWindow = GetWindow<ObstacleSelectionWindow>("Sélectionne un obstacle");
        thisWindow.troncon = tronconToApply;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        for (int i = 0; i < ObstacleManager.Instance.ListPrefabObstacles.Count + 1; i++)
        {
            if (i % colonnes == 0 && i != 0)
            {
                GUILayout.EndHorizontal();
            }

            if(i == ObstacleManager.Instance.ListPrefabObstacles.Count)
            {
                if (GUILayout.Button("Aucun", optionsButtonObstacle))
                {
                    troncon.ClearTronconFromObstacle();

                    troncon.sectionParent.AssignObstacleToScriptSection(null, troncon);

                    SaveSection(troncon.sectionParent.scriptSection);
                }
            }
            else
            {
                if(GUILayout.Button(ObstacleManager.Instance.ListPrefabObstacles[i].name, optionsButtonObstacle))
                {
                    if (!troncon.obstacle || troncon.obstacle.GetType() != ObstacleManager.Instance.ListPrefabObstacles[i].GetType())
                    {
                        troncon.ClearTronconFromObstacle();

                        GameObject newObjObstacle =
                            PrefabUtility.InstantiatePrefab(ObstacleManager.Instance.ListPrefabObstacles[i].gameObject, troncon.transform) as GameObject;
                        newObjObstacle.TryGetComponent(out troncon.obstacle);

                        troncon.sectionParent.AssignObstacleToScriptSection(ObstacleManager.Instance.ListPrefabObstacles[i], troncon);

                        SaveSection(troncon.sectionParent.scriptSection);
                    }
                }
            }

            if (i % colonnes == 0 && i != 0)
            {
                GUILayout.BeginHorizontal();
            }
        }
    }

    private void OnFocus()
    {
        
    }

    private void SaveSection(ScriptableSection scriptSection)
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(scriptSection);
        AssetDatabase.SaveAssets();
    }

    private void OnLostFocus()
    {
        Close();
    }
}
