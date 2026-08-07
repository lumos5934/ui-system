using UnityEngine;

namespace LLib
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIBase : MonoBehaviour
    {
        public Canvas Canvas { get; private set; }

        public abstract bool IsOpened { get; }

        protected virtual void Awake()
        {
            Canvas = GetComponent<Canvas>();
            
            UISystem.RegisterInstance(this);
        }

        protected virtual void OnDestroy()
        {
            UISystem.UnregisterInstance(this);
        }

        public abstract void OnOpen();
        public abstract void OnClose();
    }
}