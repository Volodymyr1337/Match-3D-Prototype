using System;
using UnityEngine;

namespace Application
{
    public abstract class BaseViewController<TView>: BaseController where TView: MonoBehaviour
    {
        protected readonly string _assetName;
        
        protected TView View { get; private set; }
        
        protected BaseViewController(string assetName)
        {
            _assetName = assetName;
        }

        public override void Initialize()
        {
            LoadView();
        }

        public override void Dispose()
        {
            if (View != null)
            {
                UnityEngine.Object.Destroy(View.gameObject);
            }
        }

        private void LoadView()
        {
            View = UnityEngine.Object.Instantiate(Resources.Load<TView>(_assetName));
        }
    }
}
