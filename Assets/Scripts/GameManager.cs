using MyUnityFramework;
using UnityEngine;

public class GameManager : SingletonAutoMono<GameManager>
{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
