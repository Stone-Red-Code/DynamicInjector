using System;
using System.Collections.Generic;

namespace DynamicInjector
{
    /// <summary>
    /// A scope
    /// </summary>
    public class LifetimeScope : IContainer, IDisposable
    {
        private readonly InjectionContainer injectionContainer;

        internal Dictionary<string, object> Services { get; } = new Dictionary<string, object>();

        internal LifetimeScope(InjectionContainer injectionContainer)
        {
            this.injectionContainer = injectionContainer;
        }

        /// <inheritdoc cref="IContainer.Resolve{T}"/>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <inheritdoc cref="IContainer.Resolve(Type)"/>
        public object Resolve(Type type)
        {
            return injectionContainer.InternalResolve(type, this);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public object Invoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method)
        {
            return injectionContainer.InternalInvoke(instance, method, this);
        }

        /// <summary>
        /// Disposes all scoped disposable services
        /// </summary>
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