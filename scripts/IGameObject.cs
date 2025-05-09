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

    void Update(GameTime gameTime, int screenWidth, int screenHeight);
    void Draw(SpriteBatch spriteBatch);
}

public class TestGameObject(Vector2 position) : IGameObject
{
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; } = position;
    public int SpriteHeight { get; private set; } = 50;
    public int SpriteWidth { get; private set;} = 50;

    private Vector2 _spriteScale;

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
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
        
        WrapPosition(screenWidth, screenHeight);
    }

    private void WrapPosition(int screenWidth, int screenHeight)
    {
        if (Position.X < 0)
        {
            Position += new Vector2 (screenWidth, 0);
        }
        else if (Position.X > screenWidth)
        {
            Position -= new Vector2 (screenWidth, 0);
        }
        if (Position.Y < 0)
        {
            Position += new Vector2 (0, screenHeight);
        }
        else if (Position.Y > screenHeight)
        {
            Position -= new Vector2 (0, screenHeight);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var viewport = spriteBatch.GraphicsDevice.Viewport;
        var screenBounds = viewport.Bounds;

        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                var offset = new Vector2(x * viewport.Width, y * viewport.Height);
                var drawPos = Position + offset;
                var spriteRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, SpriteWidth, SpriteHeight);
                if (screenBounds.Intersects(spriteRect))
                {
                    spriteBatch.Draw(Sprite, drawPos, null, Color.White, 0, Vector2.Zero, _spriteScale, SpriteEffects.None, 0);
                }
            }
        }
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

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
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

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
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
