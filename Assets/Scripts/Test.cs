using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public GameObject go;
    
    private Mouse _mouse;
    private Keyboard _keyboard;
    private Touchscreen _touchscreen;
    
    private void Start()
    {
        _mouse = Mouse.current;
        _keyboard = Keyboard.current;
        _touchscreen = Touchscreen.current;
    }
    
    private void Update()
    {
        if (_touchscreen == null)
        {
            return;
        }
        
        if (_touchscreen.touches.Count > 0)
        {
            var current = _touchscreen.touches[0];
            if (current.press.wasPressedThisFrame)
            {
                go.SetActive(!go.activeSelf);
            }
        }
    }
}
