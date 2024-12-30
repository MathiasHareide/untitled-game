using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class SceneManager
{
    private List<IGameObject> _gameObjects;

    public SceneManager()
    {
        _gameObjects = new List<IGameObject>();
    }

    public void AddGameObject(IGameObject go)
    {
        _gameObjects.Add(go);
    }

    public void RemoveGameObject(IGameObject go)
    {
        _gameObjects.Remove(go);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var go in _gameObjects)
        {
            go.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var go in _gameObjects)
        {
            go.Draw(spriteBatch);
        }
    }
}

