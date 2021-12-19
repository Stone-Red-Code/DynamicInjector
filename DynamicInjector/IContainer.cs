using System;

namespace DynamicInjector
{
    public interface IContainer
    {
        public object Invoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method);

        public object Resolve(Type type);

        public T Resolve<T>();
    }
}