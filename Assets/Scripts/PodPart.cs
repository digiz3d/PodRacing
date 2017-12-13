public class PodPart
{
    private string name;
    private int price;

    public PodPart(string name, int price)
    {
        this.name = name;
        this.price = price;
    }
}

public class EnginePodPart : PodPart
{
    private float addedMaxSpeed;
    public EnginePodPart (string name, int price, float maxSpeed) : base(name, price)
    {
        addedMaxSpeed = maxSpeed;
    }

    public float AddedMaxSpeed
    {
        get {
            return addedMaxSpeed;
        }
    }
}

public class InjectorPodPart : PodPart
{
    private float reducedTimeToFullSpeed;
    public InjectorPodPart(string name, int price, float timeToFullSpeed) : base(name, price)
    {
        reducedTimeToFullSpeed = timeToFullSpeed;
    }

    public float ReducedTimeToFullSpeed
    {
        get
        {
            return reducedTimeToFullSpeed;
        }
    }
}

public class RepulsorPodPart : PodPart
{
    private float addedHeight;
    public RepulsorPodPart(string name, int price, float height) : base(name, price)
    {
        addedHeight = height;
    }

    public float Addedheight
    {
        get
        {
            return addedHeight;
        }
    }
}
