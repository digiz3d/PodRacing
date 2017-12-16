using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Pod")]
public class Pod : ScriptableObject
{
    public float baseMaxSpeed;
    public float baseTimeToFullSpeed;
    new public string name;
    public string description;
    public EnginePodPart engine;               // increase max speed
    public InjectorPodPart injector;           // decrease time required to max speed
    public GameObject prefab;

    public void InstallPodPart(PodPart part)
    {
        EnginePodPart engine = part as EnginePodPart;
        if (engine != null)
        {
            InstallEngine(engine);
        }

        InjectorPodPart injector = part as InjectorPodPart;
        if (injector != null)
        {
            InstallInjector(injector);
        }
    }

    private void InstallEngine(EnginePodPart engine)
    {
        this.engine = engine;
    }

    private void InstallInjector(InjectorPodPart injector)
    {
        this.injector = injector;
    }

    public float GetMaxSpeed()
    {
        return baseMaxSpeed + engine.AddedMaxSpeed;
    }

    public float GetTimeToFullSpeed()
    {
        return baseTimeToFullSpeed + injector.ReducedTimeToFullSpeed;
    }
}