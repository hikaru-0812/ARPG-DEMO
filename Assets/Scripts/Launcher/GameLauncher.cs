using Manager;
using UnityEngine;
using Cinemachine;
using PlayerControl;
using RPG.Actor;

public class GameLauncher : MonoBehaviour
{
    private Transform playerNode;
    private PlayerController _playerController;

    private void Awake()
    {
        GameManager.GetInstance();
        // LuaEntrance.GetInstance();
    }

    private void Start()
    {
        // AssetBundleManager.GetInstance().LoadResourceAsync<GameObject>("player", "2B", (go) =>
        // {
        //     go = Instantiate(go);
        //     go.AddComponent<Actor>();
        // });
    //
    //     AssetBundleUpdateManager.GetInstance().Start((isSuccess) =>
    //     {
    //         // TODO：UI展示信息
    //         if (isSuccess)
    //         {
    //             print("更新结束，隐藏进度条");
    //         }
    //         else
    //         {
    //             // 网络有问题
    //             print("网络有问题");
    //         }
    //     },
    //     (message) =>
    //     {
    //         // TODO：UI展示信息
    //         // UI文字信息
    //         print(message);
    //     });
    //     AssetBundleUpdateManager.GetInstance().Dispose();
    }
}
