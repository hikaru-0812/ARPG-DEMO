using System.Collections.Generic;
using MyUnityFramework;
using UnityEngine;
using View;

namespace MVCLib
{
    public class ViewManager : SingletonAutoMono<ViewManager>
    {
        private readonly Dictionary<ViewType, BaseView> viewDictionary = new Dictionary<ViewType, BaseView>();

        public ViewManager()
        {
            // viewDictionary.Add(ViewType.TaskView, new TaskView());
        }

        public T GetView<T>(ViewType viewType) where T : BaseView
        {
            if (viewDictionary.TryGetValue(viewType, out var view)) return view as T;
            
            Debug.LogError("没有找到这个窗口！");
            return null;

        }

        public void OpenView(ViewType viewType)
        {
            if (!viewDictionary.TryGetValue(viewType, out var view))
            {
                Debug.LogError("没有找到这个窗口！");
                return;
            }
            
            view.Open();
        }

        public void CloseView(ViewType viewType)
        {
            if (!viewDictionary.TryGetValue(viewType, out var view))
            {
                Debug.LogError("没有找到这个窗口！");
                return;
            }
            
            view.Close();
        }

        public void PreLoadViews(ScenesType scenesType)
        {
            foreach (var item in viewDictionary.Values)
            {
                if (item.GetScenesType() == scenesType)
                {
                    item.PreLoad();
                }
            }
        }
    }
}
