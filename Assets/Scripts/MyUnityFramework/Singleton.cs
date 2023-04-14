/*
 * FileName:      普通类的单例模式
 * Author:        天璇
 * Date:          2021/02/25 14:58:22
 * UnityVersion:  2019.4.0f1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityFramework
{
    /// <summary>
    /// 普通类的单例模式
    /// </summary>
    public class Singleton<T> where T : new()
    {
        protected static T instance;

        public static T GetInstance()
        {
            if (instance == null)
                instance = new T();

            return instance;
        }
    }
}
