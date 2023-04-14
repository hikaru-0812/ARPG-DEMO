/*
 *FileName:      MonoBehavior的单例模式
 *Author:        天璇
 *Date:          2021/02/25 15:05:42
 *UnityVersion:  2019.4.0f1
 */

using UnityEngine;

namespace MyUnityFramework
{
    /// <summary>
    /// 继承自MonoBehaviour的单例模式类模板
    /// 继承后自动创建对象，不需要手动添加
    /// </summary>
    /// <typeparam name="T">需要的类型</typeparam>
    public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T GetInstance()
        {
            if (instance == null)
            {
                GameObject tempObj = new GameObject();
                tempObj.name = typeof(T).ToString();
                DontDestroyOnLoad(tempObj);
                instance = tempObj.AddComponent<T>();
            }

            return instance;
        }
    }
}
