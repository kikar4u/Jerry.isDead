using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    private Color colorBGDefault;
    private ScriptableLevel currentLevel;
    private GUILayoutOption[] optionsButton = new GUILayoutOption[2] { GUILayout.Height(50), GUILayout.Width(70) };
    private string levelName = "NvNivo";
    private List<ScriptableLevel> levelsToDelet = new List<ScriptableLevel>();
    Vector2 scrollListSectionPosition = new Vector2();

    private Campagne currentCampagne;

    [MenuItem("Window/Editeur de Nivo")]
    public static void OpenLevelEditorWindow()
    {
        GetWindow<LevelEditor>("Editeur de Nivo");
    }

    private void OnFocus()
    {
        colorBGDefault = GUI.backgroundColor;
    }

    private void OnDestroy()
    {

        SaveAllLevels();
    }

    private void OnLostFocus()
    {
        SaveAllLevels();
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        currentCampagne = (Campagne)EditorGUILayout.ObjectField("Campagne", currentCampagne, typeof(Campagne), false);
        GUILayout.Space(20);

        if(currentCampagne)
        {
            
            DrawLevelInterface();

            GUILayout.Space(5);

            if(currentLevel)
            {
                GUILayout.Label("Durée d'une séquence en second");
                currentLevel.sequenceDuration = EditorGUILayout.FloatField(currentLevel.sequenceDuration);

                GUILayout.Space(15);

                DrawSectionInterface();
            }
        }

    }

    private void SaveCampagne()
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(currentCampagne);
        AssetDatabase.SaveAssets();
    }

    #region INTERFACE LEVEL
    private void DrawLevelInterface()
    {
        GUILayout.Label("Liste des nivos");
        GUILayout.Space(10);

        DrawAddLevelInterface();

        GUILayout.Space(10);

        DrawLevelList();
    }

    private void DrawAddLevelInterface()
    {
        GUILayout.Label("Ajouter Nivo");

        levelName = GUILayout.TextField(levelName);

        if (GUILayout.Button(" + ", optionsButton))
        {
            ScriptableLevel newLevel = ScriptableLevel.CreateInstance<ScriptableLevel>();
            newLevel.name = levelName;
            newLevel.path = "Assets/Campagne/Levels/" + levelName + ".asset";

            while (AssetDatabase.LoadAssetAtPath(newLevel.path, typeof(ScriptableLevel)))
            {
                newLevel.name += "Otre";
                newLevel.path = "Assets/Campagne/Levels/" + newLevel.name + ".asset";

            }
            AssetDatabase.CreateAsset(newLevel, newLevel.path);
            AssetDatabase.SaveAssets();
            levelName = "NvNivo";

            currentCampagne.AddLevel(newLevel);
        }
    }

    private void DrawLevelList()
    {
        for (int i = 0; i < currentCampagne.listLevels.Count; i++)
        {
            DrawLevelUnit(currentCampagne.listLevels[i], i);
        }

        foreach(ScriptableLevel level in levelsToDelet)
        {
            level.RemoveAllSections();
            currentCampagne.listLevels.Remove(level);
            AssetDatabase.DeleteAsset(level.path);
        }

        levelsToDelet.Clear();
    }

    private void DrawLevelUnit(ScriptableLevel levelToDraw, int index)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(levelToDraw.name);

        if (GUILayout.Button("^"))
        {
            if (index > 0)
            {
                currentCampagne.MoveDownLevel(index);
            }
        }

        if (GUILayout.Button("ˇ"))
        {
            if (index < currentLevel.listSections.Count)
            {
                currentCampagne.MoveUpLevel(index);
            }
        }

        if (GUILayout.Button("Charger", optionsButton))
        {
            if (currentLevel) SaveLevel(currentLevel);
            LevelManager.Instance.UnfoldLevel(levelToDraw);
            currentLevel = levelToDraw;
        }
        if (GUILayout.Button("Supprimer", optionsButton))
        {
            if(EditorUtility.DisplayDialog("Supprimer Nivo", "T'es sûr de vouloir supprimer ce nivo ?", "Ouai ouai !", "Oups non..."))
            {
                if(levelToDraw == currentLevel)
                {
                    LevelManager.Instance.DeleteCurrentLevel();
                }
                levelsToDelet.Add(levelToDraw);
            }
        }

        GUILayout.EndHorizontal();
    }
    #endregion

    #region INTERFACE SECTION
    private void DrawSectionInterface()
    {
        GUILayout.Label("Liste des sections de " + currentLevel.name);
        GUILayout.Space(10);

        DrawListSections();
    }

    private void DrawListSections()
    {
        if (GUILayout.Button("Ajouter Section "))
        {
            currentLevel.AddSection();
            LevelManager.Instance.UnfoldLevel(currentLevel);
        }

        scrollListSectionPosition = GUILayout.BeginScrollView(scrollListSectionPosition, false, true);

        for (int i = 0; i < currentLevel.listSections.Count; i++)
        {
            DrawSectionUnit(currentLevel.listSections[i], i);
        }

        GUILayout.EndScrollView();
    }

    private void DrawSectionUnit(ScriptableSection sectionToDraw, int index)
    {
        GUILayoutOption[] optionsIndex = new GUILayoutOption[2] { GUILayout.Height(50), GUILayout.Width(30) };
        GUILayout.BeginHorizontal();

        if(GUILayout.Button("" + index, optionsIndex))
        {
            SceneView.lastActiveSceneView.LookAt(LevelManager.Instance.currentLevel.listSections[index].transform.position);
            Selection.activeGameObject = LevelManager.Instance.currentLevel.listSections[index].gameObject;
        }

        GUILayout.BeginVertical();
        GUILayout.Label(sectionToDraw.name);

        sectionToDraw.prefabSection = (GameObject)EditorGUILayout.ObjectField("Prefab", sectionToDraw.prefabSection, typeof(GameObject), false);
        GUILayout.EndVertical();

        
        if(GUILayout.Button("^", new GUILayoutOption[1] { GUILayout.Height(50) }))
        {
            if(index > 0)
            {
                currentLevel.MoveDownSection(index);
                LevelManager.Instance.UnfoldLevel(currentLevel);
            }
        }

        if (GUILayout.Button("ˇ", new GUILayoutOption[1] { GUILayout.Height(50) }))
        {
            if(index < currentLevel.listSections.Count)
            {
                currentLevel.MoveUpSection(index);
                LevelManager.Instance.UnfoldLevel(currentLevel);
            }
        }

        if (GUILayout.Button("Spprimer", optionsButton))
        {
            currentLevel.RemoveSection(index);
            LevelManager.Instance.UnfoldLevel(currentLevel);
        }

        GUILayout.EndHorizontal();
    }
    #endregion


    private void SaveLevel(ScriptableLevel levelToSave)
    {
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(levelToSave);
        if(LevelManager.Instance.currentLevel) EditorUtility.SetDirty(LevelManager.Instance.currentLevel);
        EditorUtility.SetDirty(LevelManager.Instance);
        AssetDatabase.SaveAssets();
    }

    private void SaveAllLevels()
    {
        if(currentCampagne)
        {
            foreach (ScriptableLevel level in currentCampagne.listLevels)
            {
                SaveLevel(level);
            }
            SaveCampagne();
        }
    }

    
}
