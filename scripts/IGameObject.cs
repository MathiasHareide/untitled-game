using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using lamo;
using untitled_game;

public interface IGameObject
{
    Vector2 Position { get; }
    Texture2D Sprite { get; }
    int SpriteHeight { get; }
    int SpriteWidth { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}

public class TestGameObject(Vector2 position) : IGameObject
{
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; } = position;
    public int SpriteHeight { get; private set; } = 50;
    public int SpriteWidth { get; private set;} = 50;

    private Vector2 _spriteScale;

    public void Update(GameTime gameTime)
    {
        var dir = new Vector2(0, 0);
        var speed = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? 1000f : 200f;
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            dir.Y--;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            dir.Y++;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            dir.X--;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            dir.X++;
        }
        if (dir != Vector2.Zero)
        {
            dir.Normalize();
        }
        Position += dir * (speed * ((float)gameTime.ElapsedGameTime.TotalSeconds));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, Position, null, Color.White, 0, Vector2.Zero, _spriteScale, SpriteEffects.None, 0);
    }

    public void SetSprite(Texture2D sprite)
    {
        Sprite = sprite;
        _spriteScale = new(SpriteWidth / Sprite.Width, SpriteHeight / Sprite.Height);
    }
}

public class ClickableGameObject(Vector2 position) : IGameObject
{
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; } = position;
    public int SpriteHeight { get; private set; } = 35;
    public int SpriteWidth { get; private set;} = 35;

    private Vector2 _spriteScale;

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        if (MouseIsHovering(mouseState) && MouseStateManager.Instance.StartedPressingLMBThisFrame())
        {
            Console.WriteLine("ClickableGameObject has been clicked on.");
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, Position, null, Color.White, 0, Vector2.Zero, _spriteScale, SpriteEffects.None, 0);
    }

    public void SetSprite(Texture2D sprite)
    {
        Sprite = sprite;
        _spriteScale = new(SpriteWidth / Sprite.Width, SpriteHeight / Sprite.Height);
    }

    private bool MouseIsHovering(MouseState mouseState)
    {
        if (mouseState.Position.X >= Position.X && mouseState.Position.X <= Position.X + _spriteScale.X)
        {
            if (mouseState.Position.Y >= Position.Y && mouseState.Position.Y <= Position.Y + _spriteScale.Y)
            {
                return true;
            }
        }
        return false;
    }
}

public class DragableGameObject(Vector2 position) : IGameObject
{
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; } = position;
    public int SpriteHeight { get; private set; } = 50;
    public int SpriteWidth { get; private set;} = 80;

    private Vector2 _spriteScale;
    private Vector2 _mousePositionLastFrame;
    private bool _shouldFollowMousePosition = false;

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        if (ShouldStartFollowingMousePosition())
        {
            _shouldFollowMousePosition = true;
        }
        else if (mouseState.LeftButton == ButtonState.Released)
        {
            _shouldFollowMousePosition = false;
        }
        if (_shouldFollowMousePosition)
        {
            Position += mouseState.Position.ToVector2() - _mousePositionLastFrame;
        }
        _mousePositionLastFrame = mouseState.Position.ToVector2();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, Position, null, Color.White, 0, Vector2.Zero, _spriteScale, SpriteEffects.None, 0);
    }

    public void SetSprite(Texture2D sprite)
    {
        Sprite = sprite;
        _spriteScale = new(SpriteWidth / Sprite.Width, SpriteHeight / Sprite.Height);
    }

    private bool ShouldStartFollowingMousePosition()
    {
        return MouseStateManager.Instance.IsMouseOverGameObject(this) && MouseStateManager.Instance.StartedPressingLMBThisFrame();
    }
}
