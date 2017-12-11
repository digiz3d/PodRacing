public class Pod
{
    public float timeToFullSpeed;
    public float timeToFullAcceleration;
    public float hoverHeight;

    public PodPart engine;             // decreases timeToFullSpeed
    public PodPart injector;        // increase
    public PodPart repulsor;           // increase hoverHeight
}