using System.Collections.Generic;
using UnityEngine;

public class PodManager : MonoBehaviour {
    public static PodManager instance;

    public List<Pod> collection;

    public List<Pod> defaultPods;

    public int selectedPod;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple PodManager scripts !!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (Pod pod in defaultPods)
        {
            AddPodToCollection(pod);
        }
    }

    // that way, we don't override values on the scriptable object from the editor. We can still change it in-game.
    public void AddPodToCollection(Pod pod)
    {
        Pod podToAdd = ScriptableObject.CreateInstance<Pod>();
        podToAdd.baseMaxSpeed = pod.baseMaxSpeed;
        podToAdd.baseTimeToFullSpeed = pod.baseTimeToFullSpeed;
        podToAdd.name = pod.name;
        podToAdd.description = pod.description;
        podToAdd.engine = pod.engine;
        podToAdd.injector = pod.injector;
        podToAdd.prefab = pod.prefab;
        if (!collection.Contains(pod))
        {
            collection.Add(podToAdd);
        }
    }
}
