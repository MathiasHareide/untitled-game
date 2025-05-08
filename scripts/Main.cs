using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace untitled_game;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private TestGameObject _test;
    private ClickableGameObject _clickable;
    private DragableGameObject _dragable;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1080,
            PreferredBackBufferHeight = 720
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _test = new TestGameObject(new (50, 50));
        _clickable = new ClickableGameObject(new (250, 50));
        _dragable = new DragableGameObject(new (450, 50));
        SceneManager.Instance.AddGameObject(_test);
        SceneManager.Instance.AddGameObject(_clickable);
        SceneManager.Instance.AddGameObject(_dragable);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _test.SetSprite(Content.Load<Texture2D>("whitepixel"));
        _clickable.SetSprite(Content.Load<Texture2D>("whitepixel"));
        _dragable.SetSprite(Content.Load<Texture2D>("whitepixel"));
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }
        MouseStateManager.Instance.Update();
        SceneManager.Instance.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DimGray);
        _spriteBatch.Begin(SpriteSortMode.BackToFront);
        SceneManager.Instance.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
