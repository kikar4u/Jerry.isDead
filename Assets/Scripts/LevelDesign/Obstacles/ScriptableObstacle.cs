using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ScriptableObstacle : ScriptableObject
{
   public ScriptableObstacle Copy()
    {
        System.Type typeObstacle = GetType();
        ScriptableObstacle newObstacle = (ScriptableObstacle)CreateInstance(typeObstacle);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(newObstacle);
        AssetDatabase.SaveAssets();
#endif
        return newObstacle;
    }
}
