using System.Collections.Generic;

namespace ProjectCore
{
    [AllowCreateInstance]
    public class ScenePathCache : SceneSingleton<ScenePathCache>
    {
        private const int INITIAL_CAPACITY = 1000; 
        private Dictionary<int, string> _scenePathCache = new Dictionary<int, string>(INITIAL_CAPACITY);
        private Dictionary<int, string> _fullScenePathCache = new Dictionary<int, string>(INITIAL_CAPACITY);

        internal void CacheScenePath(int instanceID, string scenePath)
        {
            _scenePathCache.Add(instanceID, scenePath);
        }

        internal void CacheFullScenePath(int instanceID, string fullScenePath)
        {
            _fullScenePathCache.Add(instanceID, fullScenePath);
        }
        
        internal bool TryGetScenePath(int instanceID, out string path)
        {
            return _scenePathCache.TryGetValue(instanceID, out path);
        }

        internal bool TryGetFullScenePath(int instanceID, out string path)
        {
            return _fullScenePathCache.TryGetValue(instanceID, out path);
        }
    }
}