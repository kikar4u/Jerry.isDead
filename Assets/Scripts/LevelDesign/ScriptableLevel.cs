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

    public void AddSection()
    {
        ScriptableSection newSection = CreateInstance<ScriptableSection>();
        newSection.name = "Section" + listSections.Count;
        listSections.Add(newSection);
    }

    public void AddSection(int index)
    {
        ScriptableSection newSection = CreateInstance<ScriptableSection>();
        newSection.name = "Section" + listSections.Count;
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
            GameObject newObjSection;
#if UNITY_EDITOR
            if(!Application.isPlaying)
                newObjSection = (GameObject)PrefabUtility.InstantiatePrefab(prefabSection, newLevel.transform);
    #endif
            else
                newObjSection = Instantiate(prefabSection, newLevel.transform);

            Section newSection;
            newObjSection.TryGetComponent(out newSection);
            newSection.InitializeSection(section);

            if (!Application.isPlaying)
            {
                if (section.obstacleLeft)
                {
                    PrefabUtility.InstantiatePrefab(section.obstacleLeft.gameObject, newSection.tronconLeft.transform);
                }
                if (section.obstacleCenter)
                {
                    PrefabUtility.InstantiatePrefab(section.obstacleCenter.gameObject, newSection.tronconCenter.transform);
                }
                if (section.obstacleRight)
                {
                    PrefabUtility.InstantiatePrefab(section.obstacleRight.gameObject, newSection.tronconRight.transform);
                }
            }

            newLevel.SpawnSectionFlat(newSection);
        }
    }
    
}
