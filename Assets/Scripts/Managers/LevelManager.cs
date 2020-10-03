using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<ScriptableLevel> listLevels = new List<ScriptableLevel>();
    public Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevel(string name)
    {
        ScriptableLevel newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

        foreach(ScriptableLevel level in listLevels)
        {
            if (level.name == name)
                name += "Otre";
        }

        newLevel.name = name;

        listLevels.Add(newLevel);
    }

    public void DeletLevel(ScriptableLevel levelToDelet)
    {
        if(listLevels.Contains(levelToDelet))
        {
            listLevels.Remove(levelToDelet);

            if (!Application.isPlaying)
                DestroyImmediate(levelToDelet);
            else
                Destroy(levelToDelet);
        }
    }

    public void UnfoldLevel(ScriptableLevel levelToLoad)
    {
        DeleteCurrentLevel();

        levelToLoad.UnfoldLevel();
    }

    public void DeleteCurrentLevel()
    {
        if (currentLevel)
        {
            if (Application.isPlaying)
                Destroy(currentLevel.gameObject);
            else
                DestroyImmediate(currentLevel.gameObject);
        }
    }
    
}
