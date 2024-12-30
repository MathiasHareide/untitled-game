using Microsoft.Xna.Framework;
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
    private Texture2D _texture;

    public Vector2 Position { get; set; }

    public TestGameObject(Texture2D texture)
    {
        _texture = texture;
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
        spriteBatch.Draw(_texture, Position, null, Color.White, 0, Vector2.Zero, 50, SpriteEffects.None, 0);
    }
}
