using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface IGameObject
{
    Vector2 Position { get; set; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}

public class TestGameObject : IGameObject
{
    private SpriteRenderer _spriteRenderer;

    public Vector2 Position { get; set; }

    public TestGameObject(Texture2D texture)
    {
        _spriteRenderer = new SpriteRenderer(texture);
    }

    public void Update(GameTime gameTime)
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

    public void Draw(SpriteBatch spriteBatch)
    {
        _spriteRenderer.Draw(spriteBatch, Position, 50);
    }
}
