using System;

namespace Source.Services.ServicesResolver
{
    public abstract class BaseService : IDisposable
    {
        protected ServiceResolver ServiceResolver { get; private set; }

        #region Constructor

        private bool _isInitialized;
        
        public BaseService(ServiceResolver serviceResolver)
        {
            ServiceResolver = serviceResolver;
        }

        public void Init()
        {
            if (_isInitialized) return;
            
            Initialize();
            
            _isInitialized = true;
        }

        #endregion

        protected bool TryGetService<TService>(out TService service) where TService : BaseService
        {
            return ServiceResolver.TryGet(out service);
        }
        
        protected abstract void Initialize();
        public abstract void Dispose();
    }
}
