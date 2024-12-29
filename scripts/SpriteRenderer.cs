using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class SpriteRenderer
{
    private Texture2D _texture;

    public SpriteRenderer(Texture2D texture)
    {
        _texture = texture;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
    {
        spriteBatch.Draw(_texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}
