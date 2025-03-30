using Raylib_cs;

class Player : GameObject
{
    public int lives;

    public Player(float x, float y, float speed, int lives)
        : base(x, y, speed)
    {
        this.lives = lives;
    }

    // Override Update to handle movement.
    public override void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            x -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            x += speed;
        }

        // Prevent the player from leaving the screen.
        if (x < 0) x = 0;
        if (x > GameManager.SCREEN_WIDTH - 50) x = GameManager.SCREEN_WIDTH - 50; // assuming width of 50
    }

    // Override Draw to render the player.
    public override void Draw()
    {
        Raylib.DrawRectangle((int)x, (int)y, 50, 20, Color.Blue);
    }
}
