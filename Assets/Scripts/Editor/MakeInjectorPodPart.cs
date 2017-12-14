using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeInjectorPodPart
{
    [MenuItem("Assets/Create/Pod part/Injector")]
    public static void Create()
    {
        InjectorPodPart asset = ScriptableObject.CreateInstance<InjectorPodPart>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptables/Injectors/NewInjectorPodPart.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}