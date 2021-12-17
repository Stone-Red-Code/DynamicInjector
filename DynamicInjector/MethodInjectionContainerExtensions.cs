using System;

namespace DynamicInjector
{
    public static class MethodInjectionContainerExtensions
    {
        public static void Invoke<TService>(this InjectionContainer injectionContainer, Func<TService, Delegate> method)
        {
            injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TInstance, TReturn>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TInstance, TReturn, T1>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<T1, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TInstance, TReturn, T1, T2>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<T1, T2, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TInstance, TReturn, T1, T2, T3>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<T1, T2, T3, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TInstance, TReturn, T1, T2, T3, T4>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<T1, T2, T3, T4, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TInstance, TReturn, T1, T2, T3, T4, T5>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<T1, T2, T3, T4, T5, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TInstance, TReturn, T1, T2, T3, T4, T5, T6>(this InjectionContainer injectionContainer, TInstance instance, Func<TInstance, Func<T1, T2, T3, T4, T5, T6, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(instance, method);
        }

        public static TReturn Invoke<TService, TReturn>(this InjectionContainer injectionContainer, Func<TService, Func<TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TService, TReturn, T1>(this InjectionContainer injectionContainer, Func<TService, Func<T1, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TService, TReturn, T1, T2>(this InjectionContainer injectionContainer, Func<TService, Func<T1, T2, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TService, TReturn, T1, T2, T3>(this InjectionContainer injectionContainer, Func<TService, Func<T1, T2, T3, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TService, TReturn, T1, T2, T3, T4>(this InjectionContainer injectionContainer, Func<TService, Func<T1, T2, T3, T4, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TService, TReturn, T1, T2, T3, T4, T5>(this InjectionContainer injectionContainer, Func<TService, Func<T1, T2, T3, T4, T5, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }

        public static TReturn Invoke<TService, TReturn, T1, T2, T3, T4, T5, T6>(this InjectionContainer injectionContainer, Func<TService, Func<T1, T2, T3, T4, T5, T6, TReturn>> method)
        {
            return (TReturn)injectionContainer.Invoke(injectionContainer.Resolve<TService>(), method);
        }
    }
}