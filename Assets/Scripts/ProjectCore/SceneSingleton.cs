using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ProjectCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowCreateInstanceAttribute : Attribute
    {

    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowNullInstanceAttribute : Attribute
    {

    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowFindInstanceIfNullAttribute : Attribute
    {

    }

    public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
    {
        public static T SceneInstance => FindOrGetInstance();

        private static T _instance;

        public static bool IsInstanceNull => _instance == null;

        private void Awake()
        {
            if (_instance == null)
            {
#if UNITY_EDITOR
                Debug.Log($"[SceneSingleton] Singleton of '{typeof(T)}' Awake and set as active singleton", gameObject);
#endif
                _instance = (T)this;
                OnSingletonInit();
            }
            else
            {
                // Seems like the _instance may be set earlier in FindOrGetInstance method
                // which result undesirable result. It's possible to reproduce on THOR_Thunderstorm scene singleton.
                // I'm pretty sure its fine to only have a simple check if the _instance equals to this class or not.
                if (!_instance.Equals(this))
                    DestroyImmediate(gameObject);
            }
        }

        protected virtual void OnSingletonInit()
        {

        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        private static T FindOrGetInstance()
        {
            if (_instance)
                return _instance;

            // Allow find instance in case of null: that should help to prevent undesired null instances
            bool allowFindInstance = typeof(T).GetCustomAttributes(typeof(AllowFindInstanceIfNullAttribute)).Any();
            // Added cuz in some cases we should allow null refs for Scene Instances.
            // Example: LevelChangeBehavior
            // When subscribed component call OnDestroy, the LevelChangeBehavior might be null.
            bool allowNull = typeof(T).GetCustomAttributes(typeof(AllowNullInstanceAttribute)).Any();
            if (allowNull && !allowFindInstance)
                return null;

            if (allowFindInstance)
                _instance = GameObjectExtensions.FindComponent<T>(true);
            if (!_instance)
            {
                bool allowCreate = typeof(T).GetCustomAttributes(typeof(AllowCreateInstanceAttribute)).Any();
                if (allowCreate)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
                else
                    throw new NullReferenceException(
                        $"[SceneSingleton] SceneSingleton without AllowCreateInstance attribute " +
                        $"cant find instance of class '{typeof(T)}' on scene");
            }

            if (_instance)
                _instance.OnSingletonInit();

            return _instance;
        }
    }
}
