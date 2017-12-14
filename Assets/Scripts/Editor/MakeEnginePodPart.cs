using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeEnginePodPart
{
    [MenuItem("Assets/Create/Pod part/Engine")]
    public static void Create()
    {
        EnginePodPart asset = ScriptableObject.CreateInstance<EnginePodPart>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptables/Engines/NewEnginePodPart.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}