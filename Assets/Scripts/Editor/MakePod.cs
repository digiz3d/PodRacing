using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakePod {
    [MenuItem("Assets/Create/Pod custom")]
    public static void Create()
    {
        Pod asset = ScriptableObject.CreateInstance<Pod>();
        AssetDatabase.CreateAsset(asset, "Assets/NewPod.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
