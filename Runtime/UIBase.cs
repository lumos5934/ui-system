using System.Collections;
using UnityEngine;

namespace LLib
{
    public abstract class UIBase : MonoBehaviour
    {
        [HideInInspector] public RectTransform rectTransform;
        private Coroutine _endUpdateCoroutine;

        public virtual bool IsOpened => gameObject.activeSelf;
        

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            UISystem.Instance.RegisterInstance(this);
        }
        
        protected virtual void EndUpdate()
        {
        }
        
        protected virtual void OnEnable()
        {
            _endUpdateCoroutine = StartCoroutine(EndUpdateCoroutine());
        }

        protected virtual void OnDisable()
        {
            if (_endUpdateCoroutine != null)
            {
                StopCoroutine(_endUpdateCoroutine);
                _endUpdateCoroutine = null;
            }
        }

        protected virtual void OnDestroy()
        {
            UISystem.Instance.UnregisterInstance(this);
        }

        public virtual void OnOpen()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnClose()
        {
            gameObject.SetActive(false);
        }
        
        private IEnumerator EndUpdateCoroutine()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();

                EndUpdate();
            }
        }
    }
}