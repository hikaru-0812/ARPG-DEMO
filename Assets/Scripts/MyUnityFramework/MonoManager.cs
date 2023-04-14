/*
 *FileName:      MonoManager.cs
 *Author:        天璇
 *Date:          2021/03/14 15:00:58
 *UnityVersion:  2019.4.0f1
 */
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace MyUnityFramework
{
    /// <summary>
    /// 1.可以提供给外部添加帧更新事件的方法
    /// 2.可以提供给外部添加协程的方法
    /// </summary>
    public class MonoManager : Singleton<MonoManager>
    {
        public MonoController monoController;

        public MonoManager()
        {
            GameObject tempObj = new GameObject("MonoController");
            monoController = tempObj.AddComponent<MonoController>();
        }

        /// <summary>
        /// 添加帧更新事件
        /// （令不继承MonoBehavior的类具备帧更新的能力）
        /// </summary>
        /// <param name="_fun"></param>
        public void AddUpdateListener(UnityAction _fun)
        {
            monoController.AddUpdateListener(_fun);
        }

        /// <summary>
        /// 移除帧更新事件
        /// </summary>
        /// <param name="_fun"></param>
        public void RemoveUpdateListener(UnityAction _fun)
        {
            monoController.RemoveUpdateListener(_fun);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return monoController.StartCoroutine(methodName);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return monoController.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return monoController.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine_Auto(IEnumerator routine)
        {
            return monoController.StartCoroutine(routine);
        }

        public void StopAllCoroutines()
        {
            monoController.StopAllCoroutines();
        }

        public void StopCoroutine(IEnumerator routine)
        {
            monoController.StartCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            monoController.StopCoroutine(routine);
        }

        public void StopCoroutine(string methodName)
        {
            monoController.StopCoroutine(methodName);
        }
    }

}