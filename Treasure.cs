using Raylib_cs;

class Treasure : GameObject
{
    public int points;

    public Treasure(float x, float y, float speed, int points)
        : base(x, y, speed)
    {
        this.points = points;
    }

    // Override Update to move the treasure downward.
    public override void Update()
    {
        y += speed;
    }

    // Override Draw to render the treasure.
    public override void Draw()
    {
        Raylib.DrawRectangle((int)x, (int)y, 20, 20, Color.Gold);
    }
}
