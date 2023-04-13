using MyUnityFramework;
using UnityEngine;

public class GameManager : SingletonAutoMono<GameManager>
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // private void Update()
    // {
    //     
    // }
}
