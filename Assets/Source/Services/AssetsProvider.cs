using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Services
{
    public class AssetsProvider : IAssetsProvider
    {
        public T Load<T>(string path)
            where T : Object
        {
            T result = Resources.Load<T>(path);

            if (result == null)
            {
                throw new ArgumentException($"Asset not found: {path}");
            }

            return result;
        }

        public T[] LoadAll<T>(string path)
            where T : Object 
        {
            T[] result = Resources.LoadAll<T>(path);
            
            if (result == null)
            {
                throw new ArgumentException($"Asset not found: {path}");
            }

            return result;
        }
    }
}
