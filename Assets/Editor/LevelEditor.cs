using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    private Color colorBGDefault;
    private ScriptableLevel currentLevel;
    private GUILayoutOption[] optionsButton = new GUILayoutOption[2] { GUILayout.Height(40), GUILayout.Width(70) };
    private string levelName = "NvNivo";
    private List<ScriptableLevel> levelsToDelet = new List<ScriptableLevel>();
    Vector2 scrollListSectionPosition = new Vector2();

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


        DrawLevelInterface();

        GUILayout.Space(10);



        if(currentLevel)
        {
            DrawSectionInterface();
        }

        
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
            LevelManager.Instance.AddLevel(levelName);
            levelName = "NvNivo";
        }
    }

    private void DrawLevelList()
    {
        foreach(ScriptableLevel level in LevelManager.Instance.listLevels)
        {
            DrawLevelUnit(level);
        }

        foreach(ScriptableLevel level in levelsToDelet)
        {
            LevelManager.Instance.DeletLevel(level);
        }

        levelsToDelet.Clear();
    }

    private void DrawLevelUnit(ScriptableLevel levelToDraw)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(levelToDraw.name);

        if(GUILayout.Button("Charger", optionsButton))
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
        GUILayoutOption[] optionsIndex = new GUILayoutOption[2] { GUILayout.Height(30), GUILayout.Width(20) };
        GUILayout.BeginHorizontal();

        if(GUILayout.Button("" + index, optionsIndex))
        {
            SceneView.lastActiveSceneView.LookAt(LevelManager.Instance.currentLevel.listSections[index].transform.position);
            Selection.activeGameObject = LevelManager.Instance.currentLevel.listSections[index].gameObject;
        }

        sectionToDraw.name = GUILayout.TextField(sectionToDraw.name);

        if(GUILayout.Button("^"))
        {
            currentLevel.MoveDownSection(index);
            LevelManager.Instance.UnfoldLevel(currentLevel);
        }

        if (GUILayout.Button("ˇ"))
        {
            currentLevel.MoveUpSection(index);
            LevelManager.Instance.UnfoldLevel(currentLevel);
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
        foreach (ScriptableLevel level in LevelManager.Instance.listLevels)
        {
            SaveLevel(level);
        }
    }

    
}
