using System.Collections.Generic;
using UnityEngine;

public class PodManager : MonoBehaviour {
    public static PodManager instance;

    public List<Pod> collection;

    public List<Pod> defaultPods;

    public int selectedPod;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (Pod pod in defaultPods)
            {
                AddPodToCollection(pod);
            }
        }
        else
        {
            Debug.LogWarning("Multiple PodManager scripts !!");
            Destroy(gameObject);
        }
    }


    // that way, we don't override values on the scriptable object from the editor. We can still change it in-game, by buying new parts.
    public void AddPodToCollection(Pod pod)
    {
        Pod podToAdd = Object.Instantiate(pod);

        if (!collection.Contains(pod))
        {
            collection.Add(podToAdd);
        }
    }

    public Pod GetSelectedPod()
    {
        return collection[selectedPod];
    }
}
