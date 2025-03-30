using Raylib_cs;

class Bomb : GameObject
{
    public int damage;

    public Bomb(float x, float y, float speed, int damage)
        : base(x, y, speed)
    {
        this.damage = damage;
    }

    // Override Update to move the bomb downward.
    public override void Update()
    {
        y += speed;
    }

    // Override Draw to render the bomb.
    public override void Draw()
    {
        Raylib.DrawCircle((int)x, (int)y, 10, Color.Red);
    }
}
