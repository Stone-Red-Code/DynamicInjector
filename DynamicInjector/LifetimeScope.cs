using System;
using System.Collections.Generic;

namespace DynamicInjector
{
    public class LifetimeScope : IContainer, IDisposable
    {
        private readonly InjectionContainer injectionContainer;

        public Dictionary<string, object> Services { get; } = new Dictionary<string, object>();

        public LifetimeScope(InjectionContainer injectionContainer)
        {
            this.injectionContainer = injectionContainer;
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            return injectionContainer.InternalResolve(type, this);
        }

        public object Invoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method)
        {
            return injectionContainer.InternalInvoke(instance, method, this);
        }

        public void Dispose()
        {
            foreach (object? service in Services.Values)
            {
                if (service is IDisposable disposableService)
                {
                    disposableService.Dispose();
                }
            }
        }
    }
}