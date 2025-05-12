using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SceneManager
{
    public static SceneManager Instance
    {
        get
        {
            _instance ??= new SceneManager();
            return _instance;
        }
    }
    private static SceneManager _instance;

    private List<IGameObject> _gameObjects = [];
    private List<IGameObject> _toAdd = [];
    private List<IGameObject> _toRemove = [];

    public void AddGameObject(IGameObject go)
    {
        _toAdd.Add(go);
    }

    public void RemoveGameObject(IGameObject go)
    {
        _toRemove.Add(go);
    }

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
    {
        foreach (var go in _toRemove)
        {
            _gameObjects.Remove(go);
        }
        _toRemove.Clear();

        _gameObjects.AddRange(_toAdd);
        _toAdd.Clear();

        foreach (var go in _gameObjects)
        {
            go.Update(gameTime, screenWidth, screenHeight);
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

