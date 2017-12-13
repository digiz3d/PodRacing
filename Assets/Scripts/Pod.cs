using System;

[Serializable]
public class Pod
{
    private float baseMaxSpeed;
    private float baseTimeToFullSpeed;
    private float baseHoverHeight;

    private EnginePodPart engine;               // increase max speed
    private InjectorPodPart injector;           // decrease time required to max speed
    private RepulsorPodPart repulsor;           // increase hover height

    public Pod(float maxSpeed, float timeToFullSpeed, float hoverHeight, EnginePodPart engine, InjectorPodPart injector, RepulsorPodPart repulsor)
    {
        baseMaxSpeed = maxSpeed;
        baseTimeToFullSpeed = timeToFullSpeed;
        baseHoverHeight = hoverHeight;

        this.engine = engine;
        this.injector = injector;
        this.repulsor = repulsor;
    }

    public void InstallPodPart (PodPart part)
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

        RepulsorPodPart repulsor = part as RepulsorPodPart;
        if (repulsor != null)
        {
            InstallRepulsor(repulsor);
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

    private void InstallRepulsor(RepulsorPodPart repulsor)
    {
        this.repulsor = repulsor;
    }

    public float GetMaxSpeed()
    {
        return baseMaxSpeed + engine.;
    }

    public float GetTimeToFullSpeed()
    {
        return baseTimeToFullSpeed + injector.Modifier;
    }

    public float GetHoverHeight()
    {
        return baseHoverHeight + repulsor.Modifier;
    }
}