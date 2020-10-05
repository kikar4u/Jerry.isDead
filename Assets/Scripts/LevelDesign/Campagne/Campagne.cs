using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCampagne", menuName = "Campagne")]
public class Campagne : ScriptableObject
{
    public List<ScriptableLevel> listLevels = new List<ScriptableLevel>();

    public void AddLevel(ScriptableLevel levelToAdd)
    {
        listLevels.Add(levelToAdd);
    }

    public void MoveUpLevel(int index)
    {
        ScriptableLevel levelToMove = listLevels[index];
        listLevels.Remove(levelToMove);

        if (index == listLevels.Count)
        {
            listLevels.Add(levelToMove);
        }
        else
        {
            listLevels.Insert(index + 1, levelToMove);
        }
    }

    public void MoveDownLevel(int index)
    {
        ScriptableLevel levelToMove = listLevels[index];
        listLevels.RemoveAt(index);

        
            listLevels.Insert(index - 1, levelToMove);
        
    }
}
