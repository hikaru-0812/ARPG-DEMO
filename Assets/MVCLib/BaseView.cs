using UnityEngine;
using UnityEngine.UI;
using View;

namespace MVCLib
{
    public enum ScenesType
    {
        Start, Main
    }

    public abstract class BaseView
    {
        protected Transform viewTransform;
        protected string resourceName;
        protected bool resident;
        protected ViewType viewType;
        protected ScenesType scenesType;

        protected bool visible;
        public bool IsVisible
        {
            get
            {
                return visible;
            }
        }

        protected Button[] buttons;

        protected BaseView(string resourceName)
        {
            this.resourceName = resourceName;
        }

        protected virtual void ViewInit()
        {
            //隐藏的也会查找
            buttons = viewTransform.GetComponentsInChildren<Button>(true);
        }

        protected virtual void AfterOpen()
        {

        }

        protected virtual void AddListener()
        {

        }

        protected virtual void RegisterUIEvent()
        {

        }

        protected virtual void RemoveListener()
        {

        }

        protected virtual void Update(float deltaTime)
        {

        }

        protected virtual void BeforeClose()
        {

        }

        public override string ToString()
        {
            return GetType().ToString().Replace("View.", "");
        }

        //----------------------WindowsManger使用的接口----------------------//
        public void Open()
        {
            if (viewTransform == null)
            {
                Creat();
            }

            if (viewTransform.gameObject.activeSelf) return;
            UIRoot.SetParent(viewTransform);
            viewTransform.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            viewTransform.gameObject.SetActive(true);
            
            ViewInit();
            AddListener();
            AfterOpen();
            //visible = true;
        }

        public void Close()
        {
            if (viewTransform.gameObject.activeSelf == true)
            {
                RemoveListener();
                BeforeClose();

                if (resident)
                {
                    viewTransform.gameObject.SetActive(false);//也可以放进对象池
                }
                else
                {
                    Object.Destroy(viewTransform.gameObject);
                    viewTransform = null; 
                }
            }
            visible = false;
        }

        public void PreLoad()
        {
            Creat();
        }

        public ViewType GetViewType()
        {
            return viewType;
        }

        public ScenesType GetScenesType()
        {
            return scenesType;
        }

        public bool IsResident()
        {
            return resident;
        }

        //----------------------内部使用----------------------//
        private bool Creat()
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return false;
            }

            // if (viewTransform != null) return true;
            var obj = Resources.Load<GameObject>(resourceName);
            if (obj == null)
            {
                Debug.LogError($"未找到预制件{this.viewType}");
                return false;
            }

            viewTransform = Object.Instantiate(obj).transform;
            viewTransform.gameObject.SetActive(false);
            UIRoot.SetParent(viewTransform);

            return true;
        }
    }
}