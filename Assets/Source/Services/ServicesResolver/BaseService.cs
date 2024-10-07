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

        protected TService GetService<TService>() where TService : class, IDisposable
        {
            return ServiceResolver.Get<TService>();
        }
        
        protected abstract void Initialize();
        public abstract void Dispose();
    }
}
