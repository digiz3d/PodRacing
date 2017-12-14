using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Pod part/Engine")]
public class EnginePodPart : PodPart
{
    public float addedMaxSpeed;
    /*
    public EnginePodPart(string name, int price, float maxSpeed) : base(name, price)
    {
        addedMaxSpeed = maxSpeed;
    }
    */
    public float AddedMaxSpeed
    {
        get
        {
            return addedMaxSpeed;
        }
    }
}