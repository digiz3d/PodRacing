using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Pod part/Injector")]
public class InjectorPodPart : PodPart
{
    public float reducedTimeToFullSpeed;
    /*
    public InjectorPodPart(string name, int price, float timeToFullSpeed) : base(name, price)
    {
        reducedTimeToFullSpeed = timeToFullSpeed;
    }
    */
    public float ReducedTimeToFullSpeed
    {
        get
        {
            return reducedTimeToFullSpeed;
        }
    }
}