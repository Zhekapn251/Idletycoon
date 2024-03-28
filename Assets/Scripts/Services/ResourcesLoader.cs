using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public static class ResourcesLoader
    {
        public static async UniTask<T> LoadResourceAsync<T>(string resourcePath) where T : Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(resourcePath);
            
            await UniTask.WaitUntil(() => request.isDone);
            
            return request.asset as T;
        }
        
        public static T LoadResource<T>(string resourcePath) where T : Object
        {
            return Resources.Load<T>(resourcePath);
        }
    }
}