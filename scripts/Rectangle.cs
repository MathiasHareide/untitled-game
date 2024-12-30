using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Rectangle : IGameObject
{    
    private Texture2D _texture;

    public Vector2 Position { get; set; }
    public Vector2 Size {get; private set;}

    public Rectangle(Texture2D texture, Vector2 size)
    {
        _texture = texture;
        Size = new Vector2(size.X / texture.Width, size.Y / texture.Height);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, null, Color.White, 0, Vector2.Zero, Size, SpriteEffects.None, 0);
    }
}
