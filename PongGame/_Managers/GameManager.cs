using System;
using Microsoft.Xna.Framework;
using System.Numerics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame;
public class GameManager
{
    private readonly Paddle _p1;
    private readonly Paddle _p2;
    private readonly Ball _ball;
    private int _score1 = 0;
    private int _score2 = 0;
    private readonly Microsoft.Xna.Framework.Vector2 _pos1;
    private readonly Microsoft.Xna.Framework.Vector2 _pos2;
    private bool _p1won;
    private bool _p2won;
    private readonly SoundEffect _sfx;

    public GameManager()
    {
        Globals.Texture = new(Globals.GraphicsDevice, 1, 1);
        Globals.Texture.SetData(new[] { Color.White});
        _sfx = Globals.Content.Load<SoundEffect>("pong");

        _pos1 = new(100, 100);
        _pos2 = new(600, 100);

        _p1 = new(new(15, Globals.WindowSize.Y / 2), Keys.W, Keys.S);
        _p2 = new(new(Globals.WindowSize.X - 15, Globals.WindowSize.Y / 2), Keys.Up, Keys.Down);

        _ball = new();
    }

    public void Reset()
    {
        _ball.Reset();
        _p1won = false;
        _p2won = false;
        _score1 = 0;
        _score2 = 0;
        _p1.Position = new(15, Globals.WindowSize.Y / 2);
        _p2.Position = new(Globals.WindowSize.X - 15, Globals.WindowSize.Y / 2);
    }

    public void CheckPaddleCollisions()
    {
        if (_p1.Body.Intersects(_ball.Body))
        {
            _ball.Direction.X *= -1;
            _ball.Position.X += _p1.Body.Right - _ball.Body.Left;
            _sfx.Play(0.1f, 0f, 0f);
        }

        if (_p2.Body.Intersects(_ball.Body))
        {
            _ball.Direction.X *= -1;
            _ball.Position.X -= _ball.Body.Right - _p2.Body.Left;
            _sfx.Play(0.1f, 0f, 0f);
        }
    }

    public void CheckScoring()
    {
        if (_ball.Body.Left < _p1.Body.Left)
        {
            _score2++;
            _ball.Reset();
        }

        if (_ball.Body.Right > _p2.Body.Right)
        {
            _score1++;
            _ball.Reset();
        }
    }

    public void CheckWin()
    {
        const int NEED = 11;

        if (_score1 >= NEED)
        {
            _p1won = true;
        }

        if (_score2 >= NEED)
        {
            _p2won = true;
        }
    }

    public void Update()
    {
        if (InputManager.EnterPressed) _p2.Ai = !_p2.Ai;

        if (_p1won || _p2won)
        {
            if (InputManager.SpacePressed) Reset();
            return;
        }

        _p1.Update(_ball);
        _p2.Update(_ball);
        _ball.Update();
        CheckPaddleCollisions();
        CheckScoring();
        CheckWin();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();
        _p1.Draw();
        _p2.Draw();
        _ball.Draw();

        Globals.SpriteBatch.End();
    }
}

