using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public abstract class GameObject
{
    public Vector2 Position { get; protected set; }

    public GameObject(Vector2 position)
    {
        Position = position;
    }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}

public class TestGameObject : GameObject
{
    private SpriteRenderer _spriteRenderer;

    public TestGameObject(Texture2D texture, Vector2 position) : base(position)
    {
        _spriteRenderer = new SpriteRenderer(texture);
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {   
            Position = new Vector2(Position.X, Position.Y - 5f);
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {   
            Position = new Vector2(Position.X, Position.Y + 5f);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {   
            Position = new Vector2(Position.X - 5f, Position.Y);
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Position = new Vector2(Position.X + 5f, Position.Y);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _spriteRenderer.Draw(spriteBatch, Position, 50);
    }
}
