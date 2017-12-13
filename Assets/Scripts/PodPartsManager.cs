using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodPartsManager : MonoBehaviour {
    public static PodPartsManager instance;

    private List<PodPart> allPodParts;

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

    private void Start()
    {
        allPodParts = new List<PodPart>
        {
            new EnginePodPart("Engine w/ 2 plugs", 2000, 200f),
            new EnginePodPart("Engine w/ 3 plugs", 4000, 300f),
            new EnginePodPart("Engine w/ 4 plugs", 8000, 400f),
            new EnginePodPart("Engine w/ 5 plugs", 12000, 500f),

            new InjectorPodPart("Injector", 1500, -5f),
            new InjectorPodPart("Turbo Injector", 3000, -10f),
            new InjectorPodPart("Nitro Injector", 4500, -15f),

            new RepulsorPodPart("Repulsor 0.6 meters", 1000, 0.1f),
            new RepulsorPodPart("Repulsor 0.7 meters", 2000, 0.2f),
            new RepulsorPodPart("Repulsor 0.8 meterss", 3000, 0.3f)
        };
    }
}
