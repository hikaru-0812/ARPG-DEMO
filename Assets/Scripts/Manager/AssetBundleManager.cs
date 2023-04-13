using System;
using System.Collections;
using System.Collections.Generic;
using MyUnityFramework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager
{
    /// <summary>
    /// AB包管理器，让外部更方便地进行资源加载
    /// </summary>
    public class AssetBundleManager : SingletonAutoMono<AssetBundleManager>
    {
        /// <summary>
        /// 主包
        /// </summary>
        private AssetBundle _mainAB;
    
        /// <summary>
        /// 依赖包信息
        /// </summary>
        private AssetBundleManifest _abManifest;
    
        /// <summary>
        /// 已经加载进内存的 ab 包
        /// </summary>
        private readonly Dictionary<string, AssetBundle> _loadedAssetBundles = new Dictionary<string, AssetBundle>();

        /// <summary>
        /// AB 包路径
        /// </summary>
        private string ABPath
        {
            get
            {
#if UNITY_STANDALONE_WIN
                return Application.dataPath + @"/../../PUNISHING Art Resources/AssetBundles/" + @"PC/";
#elif UNITY_ANDROID
            return @"D:\Unity Project\PUNISHING Art Resources\AssetBundles\Android\";
#else
            return @"D:\Unity Project\PUNISHING Art Resources\AssetBundles\IOS\";
#endif
            }
        }

        /// <summary>
        /// 主包名/平台名
        /// </summary>
        private string MainABName
        {
            get
            {
#if UNITY_STANDALONE_WIN
                return @"PC";
#elif UNITY_ANDROID
            return @"Android";
#else
            return @"IOS";
#endif
            }
        }

        private void LoadAB(string abName)
        {
            AssetBundle ab;
        
            //加载依赖信息文件
            if (_mainAB is null)
            {
                _mainAB = AssetBundle.LoadFromFile(ABPath + MainABName);
                _abManifest = _mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }

            //加载所有依赖信息
            var dependencies = _abManifest.GetAllDependencies(abName);
            foreach (var depend in dependencies)
            {
                if (_loadedAssetBundles.ContainsKey(depend)) continue;
                ab = AssetBundle.LoadFromFile(ABPath + depend);
                _loadedAssetBundles.Add(depend, ab);
            }
        
            //判读是否已经被加载，没有再加载AB包
            if (_loadedAssetBundles.ContainsKey(abName)) return;
            ab = AssetBundle.LoadFromFile(ABPath + abName);
            _loadedAssetBundles.Add(abName, ab);
        }

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public Object LoadResources(string abName, string resName)
        {
            LoadAB(abName);
        
            //加载资源
            return _loadedAssetBundles[abName].LoadAsset(resName);
        }
    
        /// <summary>
        /// 同步加载（指定类型）（Lua没有泛型，所以要传入一个type）
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Object LoadResources(string abName, string resName, Type type)
        {
            LoadAB(abName);
        
            //加载资源
            return _loadedAssetBundles[abName].LoadAsset(resName, type);
        }
    
        /// <summary>
        /// 同步加载（泛型方法）
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadResources<T>(string abName, string resName) where T : Object
        {
            LoadAB(abName);
        
            //加载资源
            return _loadedAssetBundles[abName].LoadAsset<T>(resName);
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="callback"></param>
        public void LoadResourceAsync(string abName, string resName, Action<Object> callback)
        {
            StartCoroutine(ReallyLoadResourcesAsync(abName, resName, callback));
        }

        private IEnumerator ReallyLoadResourcesAsync(string abName, string resName, Action<Object> callback)
        {
            LoadAB(abName);
        
            //加载资源
            var abr=_loadedAssetBundles[abName].LoadAssetAsync(resName);
            yield return abr;

            callback(abr.asset);
        }

        /// <summary>
        /// 异步加载（指定类型）（Lua没有泛型，所以要传入一个type）
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        public void LoadResourceAsync(string abName, string resName, Type type, Action<Object> callback)
        {
            StartCoroutine(ReallyLoadResourcesAsync(abName, resName, type, callback));
        }

        private IEnumerator ReallyLoadResourcesAsync(string abName, string resName, Type type, Action<Object> callback)
        {
            LoadAB(abName);
        
            //加载资源
            var abr=_loadedAssetBundles[abName].LoadAssetAsync(resName, type);
            yield return abr;

            callback(abr.asset);
        }
    
        /// <summary>
        /// 异步加载（泛型方法）
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="callback"></param>
        public void LoadResourceAsync<T>(string abName, string resName, Action<T> callback) where T : Object
        {
            StartCoroutine(ReallyLoadResourcesAsync<T>(abName, resName, callback));
        }

        private IEnumerator ReallyLoadResourcesAsync<T>(string abName, string resName, Action<T> callback) where T : Object
        {
            LoadAB(abName);
        
            //加载资源
            var abr=_loadedAssetBundles[abName].LoadAssetAsync<T>(resName);
            yield return abr;

            callback(abr.asset as T);
        }
    
        /// <summary>
        /// 单个包卸载
        /// </summary>
        /// <param name="abName"></param>
        public void UnLoad(string abName)
        {
            if (!_loadedAssetBundles.ContainsKey(abName)) return;
            _loadedAssetBundles[abName].Unload(false);
            _loadedAssetBundles.Remove(abName);
        }
    
        /// <summary>
        /// 所有包卸载
        /// </summary>
        public void ClearAB()
        {
            AssetBundle.UnloadAllAssetBundles(false);
            _loadedAssetBundles.Clear();
            _mainAB = null;
            _abManifest = null;
        }
    }
}