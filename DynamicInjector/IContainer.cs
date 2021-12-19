using System;

namespace DynamicInjector
{
    /// <summary>
    /// Container
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Invoke method in class and resolve services
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        /// <param name="method"></param>
        /// <returns>The result of the specified method</returns>
        public object Invoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method);

        /// <summary>
        /// Returns an instance of a class with all resolved services from the service container
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A class with all resolved services</returns>
        public object Resolve(Type type);

        /// <summary>
        /// Returns an instance of a class with all resolved services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A class with all resolved services</returns>
        public T Resolve<T>();
    }
}