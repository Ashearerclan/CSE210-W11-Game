using System;
using System.Collections.Generic;
using Raylib_cs;

class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    private string _title;
    private Player _player;
    private List<GameObject> _gameObjects;
    private int _score;
    private Random _rand;

    public GameManager()
    {
        _title = "CSE 210 Game";
        _score = 0;
        _rand = new Random();
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            HandleInput();
            ProcessActions();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawElements();

            Raylib.EndDrawing();
        }

        // Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        // Initialize the player at the bottom center of the screen.
        _player = new Player(SCREEN_WIDTH / 2 - 25, SCREEN_HEIGHT - 30, 5, 3);
        _gameObjects = new List<GameObject>();
    }

    /// <summary>
    /// Responds to any input from the user.
    /// (Player movement is handled in the Player.Update method.)
    /// </summary>
    private void HandleInput()
    {
        // No additional input handling is needed here.
    }

    /// <summary>
    /// Processes actions such as moving objects, handling collisions, and spawning items.
    /// </summary>
    private void ProcessActions()
    {
        // Update the player.
        _player.Update();

        // Randomly create falling objects (treasures or bombs).
        if (_rand.NextDouble() < 0.02) // Approximately 2% chance per frame.
        {
            if (_rand.Next(0, 2) == 0)
            {
                // Create a Treasure.
                int points = _rand.Next(5, 15);
                float speed = (float)_rand.NextDouble() * 3 + 1;
                float x = (float)_rand.Next(0, SCREEN_WIDTH - 20);
                _gameObjects.Add(new Treasure(x, 0, speed, points));
            }
            else
            {
                // Create a Bomb.
                int damage = 1;
                float speed = (float)_rand.NextDouble() * 3 + 1;
                float x = (float)_rand.Next(0, SCREEN_WIDTH - 20);
                _gameObjects.Add(new Bomb(x, 0, speed, damage));
            }
        }

        // Update falling objects and check for collisions.
        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            _gameObjects[i].Update();

            // Check collision with the player.
            if (_gameObjects[i] is Treasure)
            {
                if (CheckCollision(_gameObjects[i], _player))
                {
                    Treasure t = (Treasure)_gameObjects[i];
                    _score += t.points;
                    _gameObjects.RemoveAt(i);
                    continue;
                }
            }
            else if (_gameObjects[i] is Bomb)
            {
                if (CheckCollision(_gameObjects[i], _player))
                {
                    Bomb b = (Bomb)_gameObjects[i];
                    _player.lives -= b.damage;
                    _gameObjects.RemoveAt(i);

                    // If the player loses all lives, end the game.
                    if (_player.lives <= 0)
                    {
                        Raylib.CloseWindow();
                        return;
                    }
                    continue;
                }
            }

            // Remove the object if it falls off the screen.
            if (_gameObjects[i].y > SCREEN_HEIGHT)
            {
                _gameObjects.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Draws all game elements on the screen.
    /// </summary>
    private void DrawElements()
    {
        // Draw the player.
        _player.Draw();

        // Draw falling objects.
        foreach (GameObject obj in _gameObjects)
        {
            obj.Draw();
        }

        // Draw score and lives.
        Raylib.DrawText("Score: " + _score, 10, 10, 20, Color.Black);
        Raylib.DrawText("Lives: " + _player.lives, 10, 40, 20, Color.Black);
    }

    /// <summary>
    /// Checks collision between a game object and the player.
    /// </summary>
    private bool CheckCollision(GameObject obj, Player player)
    {
        // Define the player's rectangle (assuming a width of 50 and a height of 20).
        Rectangle playerRect = new Rectangle(player.x, player.y, 50, 20);
        Rectangle objRect;

        if (obj is Treasure)
        {
            // Treasure is drawn as a 20x20 square.
            objRect = new Rectangle(obj.x, obj.y, 20, 20);
        }
        else if (obj is Bomb)
        {
            // Bomb is approximated as a 20x20 square.
            objRect = new Rectangle(obj.x - 10, obj.y - 10, 20, 20);
        }
        else
        {
            objRect = new Rectangle(obj.x, obj.y, 20, 20);
        }

        return Raylib.CheckCollisionRecs(playerRect, objRect);
    }
}
