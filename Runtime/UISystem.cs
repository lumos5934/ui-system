using System;
using System.Collections.Generic;

namespace LLib
{
    public static class UISystem
    {
        private static readonly Dictionary<Type, UIBase> _prefabMap = new();
        private static readonly Dictionary<Type, UIBase> _instanceMap = new();


        public static void RegisterPrefab(UIBase prefab)
        {
            _prefabMap[prefab.GetType()] = prefab;
        }

        internal static void RegisterInstance(UIBase ui)
        {
            _instanceMap[ui.GetType()] = ui;
        }

        internal static void UnregisterInstance(UIBase ui)
        {
            if (_instanceMap.TryGetValue(ui.GetType(), out var current) && current == ui)
            {
                _instanceMap.Remove(ui.GetType());
            }
        }

        public static T Open<T>() where T : UIBase
        {
            var type = typeof(T);

            if (!_instanceMap.TryGetValue(type, out var ui))
            {
                ui = Create(type);
            }

            ui.OnOpen();

            return (T)ui;
        }

        public static void Close<T>() where T : UIBase
        {
            if (!_instanceMap.TryGetValue(typeof(T), out var ui))
                return;

            ui.OnClose();
        }

        public static bool TryGet<T>(out T ui) where T : UIBase
        {
            if (_instanceMap.TryGetValue(typeof(T), out var value))
            {
                ui = (T)value;
                return true;
            }

            ui = null;
            return false;
        }

        public static T Get<T>() where T : UIBase
        {
            return TryGet(out T ui) ? ui : null;
        }

        private static UIBase Create(Type type)
        {
            if (!_prefabMap.TryGetValue(type, out var prefab))
            {
                throw new InvalidOperationException($"UI prefab is not registered: {type.Name}");
            }

            return UnityEngine.Object.Instantiate(prefab);
        }
    }
}