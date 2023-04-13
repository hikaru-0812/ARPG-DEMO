/*
 *FileName:      MonoController.cs
 *Author:        天璇
 *Date:          2021/03/14 15:00:58
 *UnityVersion:  2019.4.0f1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyUnityFramework
{
    /// <summary>
    /// 1.生命周期函数
    /// 2.事件
    /// 3.协程
    /// </summary>
    public class MonoController : MonoBehaviour
    {
        private event UnityAction UpdateEvent;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            UpdateEvent?.Invoke();
        }

        /// <summary>
        /// 添加帧更新事件
        /// </summary>
        /// <param name="_fun"></param>
        public void AddUpdateListener(UnityAction _fun)
        {
            UpdateEvent += _fun;
        }

        /// <summary>
        /// 移除帧更新事件
        /// </summary>
        /// <param name="_fun"></param>
        public void RemoveUpdateListener(UnityAction _fun)
        {
            UpdateEvent -= _fun;
        }
    }
}
