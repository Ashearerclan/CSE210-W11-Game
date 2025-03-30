using Raylib_cs;

abstract class GameObject
{
    public float x, y, speed;

    public GameObject(float x, float y, float speed)
    {
        this.x = x;
        this.y = y;
        this.speed = speed;
    }

    // Virtual methods to be overridden by derived classes.
    public virtual void Update() { }
    public virtual void Draw() { }
}
