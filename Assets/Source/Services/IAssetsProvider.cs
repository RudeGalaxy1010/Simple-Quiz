using Source.Infrastructure;
using UnityEngine;

namespace Source.Services
{
    public interface IAssetsProvider : IService
    {
        public T Load<T>(string path) where T : Object;
        public T[] LoadAll<T>(string path) where T : Object;
    }
}
