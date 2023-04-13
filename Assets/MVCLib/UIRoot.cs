using UnityEngine;

namespace MVCLib
{
    public static class UIRoot
    {
        private static readonly Transform mainView = GameObject.Find("Canvas/MainView/Center").transform;

        public static void SetParent(Transform view)
        {
            view.SetParent(mainView);
        }
    }
}
