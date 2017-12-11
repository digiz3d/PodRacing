using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodsManager : MonoBehaviour {
    public static PodsManager instance;
    public List<Pod> collection;

    private void Awake()
    {
            if (instance != null)
            {
                Debug.LogWarning("Multiple MoneyManager scripts !!");
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
