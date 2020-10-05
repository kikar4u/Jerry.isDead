using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScriptableLevel : ScriptableObject
{
    [SerializeField] private GameObject prefabSection;
    [SerializeField] private GameObject prefabLevel;
    public List<ScriptableSection> listSections = new List<ScriptableSection>();
    public float sequenceDuration = 1;

    public void AddSection()
    {
        ScriptableSection newSection = CreateInstance<ScriptableSection>();
        newSection.name = "Section" + listSections.Count;
        newSection.prefabSection = prefabSection;
        listSections.Add(newSection);
    }

    public void AddSection(int index)
    {
        ScriptableSection newSection = CreateInstance<ScriptableSection>();
        newSection.name = "Section" + listSections.Count;
        newSection.prefabSection = prefabSection;
        listSections.Insert(index, newSection);
    }

    public void MoveUpSection(int index)
    {
        ScriptableSection sectionToMove = listSections[index];
        listSections.Remove(sectionToMove);

        if(index == listSections.Count)
        {
            listSections.Add(sectionToMove);
        }
        else
        {
            listSections.Insert(index + 1, sectionToMove);
        }
    }

    public void MoveDownSection(int index)
    {
        ScriptableSection sectionToMove = listSections[index];
        listSections.Remove(sectionToMove);

        if (index == listSections.Count)
        {
            listSections.Add(sectionToMove);
        }
        else
        {
            listSections.Insert(index -1 , sectionToMove);
        }
    }

    public void RemoveSection(int index)
    {
        listSections.RemoveAt(index);
    }

    public void UnfoldLevel()
    {
        GameObject newObjLevel;
#if UNITY_EDITOR
        if (!Application.isPlaying)
            newObjLevel = (GameObject)PrefabUtility.InstantiatePrefab(prefabLevel);
#endif
        else
        newObjLevel = Instantiate(prefabLevel);
        newObjLevel.name = name;

        Level newLevel;

        newObjLevel.TryGetComponent(out newLevel);

        LevelManager.Instance.currentLevel = newLevel;

        foreach (ScriptableSection section in listSections)
        {
            if (section.prefabSection == null) section.prefabSection = prefabSection;

            GameObject newObjSection;
#if UNITY_EDITOR
            if(!Application.isPlaying)
                newObjSection = (GameObject)PrefabUtility.InstantiatePrefab(section.prefabSection, newLevel.transform);
    #endif
            else
                newObjSection = Instantiate(section.prefabSection, newLevel.transform);

            Section newSection;
            newObjSection.TryGetComponent(out newSection);
            newSection.InitializeSection(section);

            if (!Application.isPlaying)
            {
                if (section.obstacleLeft)
                {
#if UNITY_EDITOR
                    GameObject newObjObstacle = (GameObject)PrefabUtility.InstantiatePrefab(
                        ObstacleManager.Instance.GetObstacleFromScript(section.obstacleLeft).gameObject, newSection.tronconLeft.transform);
#else
                    GameObject newObjObstacle = Instantiate(ObstacleManager.Instance.GetObstacleFromScript(section.obstacleLeft).gameObject, newSection.tronconLeft.transform);
#endif
                    newObjObstacle.TryGetComponent(out newSection.tronconLeft.obstacle);

                    newSection.tronconLeft.obstacle.scriptObstacle = section.obstacleLeft;
                    newSection.tronconLeft.obstacle.LoadScriptobstacle();
                }
                if (section.obstacleCenter)
                {
#if UNITY_EDITOR
                    GameObject newObjObstacle = (GameObject)PrefabUtility.InstantiatePrefab(
                        ObstacleManager.Instance.GetObstacleFromScript(section.obstacleCenter).gameObject, newSection.tronconCenter.transform);
#else
                    GameObject newObjObstacle = Instantiate(ObstacleManager.Instance.GetObstacleFromScript(section.obstacleCenter).gameObject, newSection.tronconCenter.transform);
#endif

                    newObjObstacle.TryGetComponent(out newSection.tronconCenter.obstacle);

                    newSection.tronconCenter.obstacle.scriptObstacle = section.obstacleCenter;
                    newSection.tronconCenter.obstacle.LoadScriptobstacle();
                }
                if (section.obstacleRight)
                {
#if UNITY_EDITOR
                    GameObject newObjObstacle = (GameObject)PrefabUtility.InstantiatePrefab(
                        ObstacleManager.Instance.GetObstacleFromScript(section.obstacleRight).gameObject, newSection.tronconRight.transform);
#else
                    GameObject newObjObstacle = Instantiate(ObstacleManager.Instance.GetObstacleFromScript(section.obstacleRight).gameObject, newSection.tronconRight.transform);
#endif

                    newObjObstacle.TryGetComponent(out newSection.tronconRight.obstacle);

                    newSection.tronconRight.obstacle.scriptObstacle = section.obstacleRight;
                    newSection.tronconRight.obstacle.LoadScriptobstacle();
                }
            }

            newLevel.SpawnSectionFlat(newSection);
        }
    }
    
}
