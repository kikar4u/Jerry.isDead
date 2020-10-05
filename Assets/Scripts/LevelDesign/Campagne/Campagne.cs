using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCampagne", menuName = "Campagne")]
public class Campagne : ScriptableObject
{
    public List<ScriptableLevel> listLevel = new List<ScriptableLevel>();
}
