using System;
using Cysharp.Threading.Tasks;

namespace Source.Services.AssetBundle
{
    public interface IAssetBundleService : IDisposable
    {
        UniTask<T> LoadAsset<T>(string name) where T : UnityEngine.Object;
    }
}