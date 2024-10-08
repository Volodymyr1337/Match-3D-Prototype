using System;

namespace Source.Application
{
    public abstract class BaseModel<T> where T: class
    {
        public static event Action<T> OnModelUpdated;

        public void UpdateModel()
        {
            OnModelUpdated?.Invoke(this as T);
        }
    }
}