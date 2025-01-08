using System;
using Microsoft.Xna.Framework.Input;

public class MouseStateManager
{
    public static MouseStateManager Instance
    {
        get
        {
            _instance ??= new MouseStateManager();
            return _instance;
        }
    }
    private static MouseStateManager _instance;
    private static Action OnPressLMB;

    private MouseState _mouseState;
    private MouseState _mouseStateLastFrame;

    public MouseState MouseState => _mouseState;
    public MouseState MouseStateLastFrame => _mouseStateLastFrame;

    public void Update()
    {
        _mouseStateLastFrame = _mouseState;
        _mouseState = Mouse.GetState();

        if (StartedPressingLMBThisFrame())
        {
            OnPressLMB?.Invoke();
        }
    }

    public bool IsMouseOverGameObject(IGameObject gameObject)
    {
        var mousePos = _mouseState.Position;
        if (mousePos.X >= gameObject.Position.X && mousePos.X <= gameObject.Position.X + gameObject.SpriteWidth)
        {
            if (mousePos.Y >= gameObject.Position.Y && mousePos.Y <= gameObject.Position.Y + gameObject.SpriteHeight)
            {
                return true;
            }
        }
        return false;
    }

    private bool StartedPressingLMBThisFrame()
    {
        if (_mouseState.LeftButton == ButtonState.Pressed && _mouseStateLastFrame.LeftButton == ButtonState.Released)
        {
            return true;
        }
        return false;
    }
}