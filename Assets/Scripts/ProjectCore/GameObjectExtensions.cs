using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectCore
{
    public static class GameObjectExtensions
    {
        // can be changed any time. pretty sure it will be less than 250 in most cases so 100 also should be fine
        private static readonly int _maxFoundObjects = 100;

        public static T GetComponentInParentRecursively<T>(this GameObject gameObject) where T : Component
        {
            var parentGameObject = gameObject.transform.parent;
            while (parentGameObject)
            {
                if(parentGameObject.TryGetComponent(out T result))
                    return result;
                
                parentGameObject = parentGameObject.parent;
            }
            
            return null;
        }

        public static List<GameObject> FindGameObjectsWithTag(string tag, bool includeInactive = false)
        {
            var findResults = new List<GameObject>(_maxFoundObjects);

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

                void SearchGameObjectInChild(GameObject target, string childTag, List<GameObject> output)
                {
                    if (target.CompareTag(childTag) && (target.activeInHierarchy || includeInactive))
                        output.Add(target);

                    int children = target.transform.childCount;

                    for (int childIndex = 0; childIndex < children; childIndex++)
                        SearchGameObjectInChild(target.transform.GetChild(childIndex).gameObject, childTag, output);
                }

                foreach (var item in gameObjects)
                    SearchGameObjectInChild(item, tag, findResults);
            }

            findResults.TrimExcess();
            return findResults;
        }

        public static T FindComponent<T>(bool includeInactive = false) where T : class
        { 
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

                T SearchComponentsInChild(GameObject target)
                {
                    var components = target.GetComponent<T>();
                    if (components != null && (target.activeInHierarchy || includeInactive))
                        return components;

                    int children = target.transform.childCount;
                    for (int i = 0; i < children; i++)
                    {
                        var output = SearchComponentsInChild(target.transform.GetChild(i).gameObject);
                        if (output != null)
                            return output;
                    }

                    return null;
                }

                foreach (var item in gameObjects)
                {
                    var output = SearchComponentsInChild(item);
                    if (output != null)
                        return output;
                }
            }

            return null;
        }

        public static bool HasComponent<T>(this Scene scene)
        {
            if (!scene.isLoaded)
                return false; 
            var gameObjects = scene.GetRootGameObjects();

            bool SearchComponentsInChild(GameObject target)
            {
                var components = target.GetComponents<T>();
                if (components.Length > 0)
                    return true;

                int children = target.transform.childCount;
                for (int i = 0; i < children; i++)
                {
                    if (SearchComponentsInChild(target.transform.GetChild(i).gameObject))
                        return true;
                }

                return false;
            }

            return gameObjects.Any(SearchComponentsInChild);
        }

        public static List<T> FindComponents<T>(this Scene scene, bool includeInactive = false)
        {
            if (!scene.isLoaded)
                return new List<T>();
            var findResults = new List<T>(_maxFoundObjects);
            var gameObjects = scene.GetRootGameObjects();

            void SearchComponentsInChild(GameObject target, List<T> output)
            {
                var components = target.GetComponents<T>();
                if (target.activeInHierarchy || includeInactive)
                    output.AddRange(components);

                int children = target.transform.childCount;
                for (int i = 0; i < children; i++)
                {
                    SearchComponentsInChild(target.transform.GetChild(i).gameObject, output);
                }
            }

            foreach (var item in gameObjects) 
                SearchComponentsInChild(item, findResults); 

            findResults.TrimExcess();
            return findResults;
        }

        public static List<T> FindComponents<T>(bool includeInactive = false)
        {
            var findResults = new List<T>(_maxFoundObjects);

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

                void SearchComponentsInChild(GameObject target, List<T> output)
                {
                    var components = target.GetComponents<T>();
                    if (target.activeInHierarchy || includeInactive)
                        output.AddRange(components);

                    int children = target.transform.childCount;
                    for (int i = 0; i < children; i++)
                    {
                        SearchComponentsInChild(target.transform.GetChild(i).gameObject, output);
                    }
                }

                foreach (var item in gameObjects)
                {
                    SearchComponentsInChild(item, findResults);
                }
            }

            findResults.TrimExcess();
            return findResults;
        }

        public static string GetScenePath(this GameObject obj)
        {
            var instanceID = obj.GetInstanceID();
            if(ScenePathCache.SceneInstance.TryGetScenePath(instanceID, out var result))
                return result;

            result = GetScenePath_Private(obj);
            ScenePathCache.SceneInstance.CacheScenePath(instanceID, result);
            return result;
        }

        private static string GetScenePath_Private(this GameObject obj)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }
            return path;
        }

        public static string GetFullScenePath(this GameObject obj)
        {
            var instanceID = obj.GetInstanceID();
            if(ScenePathCache.SceneInstance.TryGetFullScenePath(instanceID, out var result))
                return result;

            result = GetFullScenePath_Private(obj);
            ScenePathCache.SceneInstance.CacheFullScenePath(instanceID, result);
            return result;
        }
        
        private static string GetFullScenePath_Private(this GameObject obj)
        {
            if (obj == null)
                return "INVALID_PATH_EXCEPTION_GAMEOBJECT_IS_NULL";
            
            string path = $"/{obj.name}";
            while (obj.transform.parent)
            {
                obj = obj.transform.parent.gameObject;
                path = $"/{obj.name + path}";
            }
            path = $"{obj.scene.name}/{path}";
            return path;
        }

        public static List<(Vector3, Quaternion, Vector3)> GetGameObjectTransformsHierarchy(this GameObject obj)
        {
            List<(Vector3, Quaternion, Vector3)> sizes = new List<(Vector3, Quaternion, Vector3)>();
            sizes.Add((obj.transform.localPosition, obj.transform.localRotation, obj.transform.localScale));
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                sizes.Add((obj.transform.localPosition, obj.transform.localRotation, obj.transform.localScale));
            }
            sizes.Reverse();
            return sizes;
        }

        public static GameObject GetRoot(this GameObject obj)
        {
            GameObject root = obj;
            
            while (root.transform.parent != null)
                root = root.transform.parent.gameObject;
            
            return root;
        }

        public static string GetRelativePath(this Component obj, Transform parent)
        {
            if (obj.transform == parent)
                return obj.gameObject.name + "/" + obj.GetType().Name;

            if (parent != null && !obj.transform.IsChildOf(parent))
            {
                Debug.LogError($"{obj.transform.name} is not a child of {parent}");
                return null;
            }

            var go = obj.gameObject;
            string path = "/" + go.name + "/" + obj.GetType().Name;
            while (go.transform != parent && go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                path = "/" + go.name + path;
            }
            return path.Substring(1);
        }

        public static string GetRelativeHierarchyPath(this Component obj, Transform parent)
        {
            if (obj.transform == parent)
                return string.Empty;

            if (parent != null && !obj.transform.IsChildOf(parent))
            {
                Debug.LogError($"{obj.transform.name} is not a child of {parent}");
                return null;
            }

            var go = obj.gameObject;
            string path = go.name;
            while (go.transform != parent && go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                if (go.transform == parent)
                    break;
                path = go.name + "/" + path;
            }
            return path;
        }

        public static GameObject CreateChild(this GameObject go, string path)
        {
            string[] subpath = path.Split('/');
            var currentGo = go;
            foreach (var item in subpath)
            {
                var existing = currentGo.transform.Find(item);
                if(existing)
                {
                    currentGo = existing.gameObject;
                    continue;
                }
                GameObject obj = new GameObject(item);
#if UNITY_EDITOR
                UnityEditor.Undo.RegisterCreatedObjectUndo(obj, "Create subchild");
#endif
                obj.transform.SetParent(currentGo.transform);
                currentGo = obj;
            }
            return currentGo;
        }
        
        public static GameObject CreateChild(this GameObject go, 
            GameObject source, 
            List<(Vector3, Quaternion, Vector3)> transforms)
        {
            List<string> subpath = new List<string>(source.GetScenePath().Split('/'));
            if(string.IsNullOrEmpty(subpath[0]))
                subpath.RemoveAt(0);
            var currentGo = go;
            for(int subpathIndex = 0; subpathIndex < subpath.Count; subpathIndex++)
            {
                var existing = currentGo.transform.Find(subpath[subpathIndex]);
                if(existing)
                {
                    currentGo = existing.gameObject;
                    continue;
                }
                GameObject obj = new GameObject(subpath[subpathIndex]);
#if UNITY_EDITOR
                UnityEditor.Undo.RegisterCreatedObjectUndo(obj, "Create subchild");
#endif
                obj.transform.SetParent(currentGo.transform);
                obj.transform.localPosition = transforms[subpathIndex].Item1;
                obj.transform.localRotation = transforms[subpathIndex].Item2;
                obj.transform.localScale = transforms[subpathIndex].Item3;
                currentGo = obj;
            }
            return currentGo;
        }

        public static List<GameObject> FindGameObjectsWithName(string name, bool includeInactive = false)
        {
            List<GameObject> findResults = new List<GameObject>(_maxFoundObjects);

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

                void SearchGameObjectInChild(GameObject target, string childName, List<GameObject> output)
                {
                    if (target.name.Equals(childName) && (target.activeInHierarchy || includeInactive))
                        output.Add(target);

                    int children = target.transform.childCount;

                    for (int childIndex = 0; childIndex < children; childIndex++)
                        SearchGameObjectInChild(target.transform.GetChild(childIndex).gameObject, childName, output);
                }

                foreach (var item in gameObjects)
                    SearchGameObjectInChild(item, name, findResults);
            }

            findResults.TrimExcess();
            return findResults;
        }

        public static List<GameObject> FindGameObjectsWithComponent<T>(bool includeInactive = false)
        {
            var findResults = new List<GameObject>(_maxFoundObjects);

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

                void SearchComponentsInChild(GameObject target, List<GameObject> output)
                {
                    if (target.TryGetComponent(out T component) && (target.activeInHierarchy || includeInactive))
                        output.Add(target);

                    var children = target.transform.childCount;

                    for (int childIndex = 0; childIndex < children; childIndex++)
                        SearchComponentsInChild(target.transform.GetChild(childIndex).gameObject, output);
                }

                foreach (var item in gameObjects)
                    SearchComponentsInChild(item, findResults);
            }

            findResults.TrimExcess();
            return findResults;
        }

        public static GameObject FindFirstGameObjectWithComponent<T>(bool includeInactive = false)
        {
            GameObject findResult = null;
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

                void SearchComponentsInChild(GameObject target, out GameObject output)
                {
                    output = null;
                    if (target.TryGetComponent(out T component) && (target.activeInHierarchy || includeInactive))
                    {
                        output = target;
                        return;
                    }

                    var children = target.transform.childCount;

                    for (int childIndex = 0; childIndex < children; childIndex++)
                    {
                        SearchComponentsInChild(target.transform.GetChild(childIndex).gameObject, out output);
                        if(output != null)
                            return;
                    }
                }

                foreach (var item in gameObjects)
                    SearchComponentsInChild(item, out findResult);
            }

            return findResult;
        }

        public static GameObject GetGameObjectByPath(string path)
        {
            string[] pathParts = path.Split('/');
            int pathMaxDepth = pathParts.Length - 1;
            string lastPathPart = pathParts[pathMaxDepth];
            List<GameObject> searchResult = new List<GameObject>(1);

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();
                int pathDepth = 0;
                
                void SearchGameObjectsInChild(GameObject target, List<GameObject> result)
                {
                    if(pathDepth > pathMaxDepth)
                        return;
                    
                    if(!target.name.Equals(pathParts[pathDepth]))
                        return;
                    
                    if(pathDepth == pathMaxDepth && target.name.Equals(lastPathPart))
                        result.Add(target);

                    var children = target.transform.childCount;
                    for (int childIndex = 0; childIndex < children; childIndex++)
                    {
                        var oldDepth = pathDepth;
                        pathDepth++;
                        SearchGameObjectsInChild(target.transform.GetChild(childIndex).gameObject, result);
                        pathDepth = oldDepth;
                    }
                }

                foreach (var item in gameObjects)
                {
                    pathDepth = 0;
                    SearchGameObjectsInChild(item, searchResult);
                }
            }

            searchResult.TrimExcess();
            return searchResult.Count < 1 ? null : searchResult[0];
        }

        public static MonoBehaviour GetComponentByPath(string path, string type, bool includeInactive = false)
        {
            string[] pathParts = path.Split('/');
            int pathMaxDepth = pathParts.Length - 1;
            string lastPathPart = pathParts[pathMaxDepth];
            List<MonoBehaviour> searchResult = new List<MonoBehaviour>(1);

            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
            {
                var gameObjects = SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();
                int pathDepth = 0;
                
                void SearchComponentsInChild(GameObject target, List<MonoBehaviour> result)
                {
                    if(pathDepth > pathMaxDepth)
                        return;
                    
                    if(!target.name.Equals(pathParts[pathDepth]))
                        return;
                    
                    if(pathDepth == pathMaxDepth && target.name.Equals(lastPathPart))
                    {
                        var components = target.GetComponents<MonoBehaviour>();
                        if (target.activeInHierarchy || includeInactive)
                        {
                            foreach (var component in components)
                            {
                                if (!component.GetType().ToString().Contains(type))
                                    continue;
                                
                                result.Add(component);
                                return;
                            }
                        }
                    }

                    var children = target.transform.childCount;
                    for (int childIndex = 0; childIndex < children; childIndex++)
                    {
                        var oldDepth = pathDepth;
                        pathDepth++;
                        SearchComponentsInChild(target.transform.GetChild(childIndex).gameObject, result);
                        pathDepth = oldDepth;
                    }
                }

                foreach (var item in gameObjects)
                {
                    pathDepth = 0;
                    SearchComponentsInChild(item, searchResult);
                }
            }

            searchResult.TrimExcess();
            return searchResult.Count < 1 ? null : searchResult[0];
        }
        
        /// <summary>
        /// Checks if a GameObject has been destroyed.
        /// </summary>
        /// <param name="gameObject">GameObject reference to check for destructedness</param>
        /// <returns>If the game object has been marked as destroyed by UnityEngine</returns>
        public static bool IsDestroyed(this GameObject gameObject)
        {
            // UnityEngine overloads the == opeator for the GameObject type
            // and returns null when the object has been destroyed, but 
            // actually the object is still there but has not been cleaned up yet
            // if we test both we can determine if the object has been destroyed.
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }
    }
}