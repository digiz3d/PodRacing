using System.Collections.Generic;
using UnityEngine;

public class PodManager : MonoBehaviour {
    public static PodManager instance;

    public List<Pod> collection;
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
    }
    
    public void AddPodToCollection(Pod pod)
    {
        if (!collection.Contains(pod))
        {
            collection.Add(pod);
        }
    }
}
