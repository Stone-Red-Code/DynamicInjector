using System;

namespace DynamicInjector
{
    /// <summary>
    /// Extension methods for <see cref="IContainer"/>
    /// </summary>
    public static class MethodInjectionContainerExtensions
    {
        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static void Invoke<T>(this IContainer injectionContainer, Func<T, Delegate> method)
        {
            injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult>(this IContainer injectionContainer, T instance, Func<T, Func<TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1>(this IContainer injectionContainer, T instance, Func<T, Func<T1, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2>(this IContainer injectionContainer, T instance, Func<T, Func<T1, T2, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3>(this IContainer injectionContainer, T instance, Func<T, Func<T1, T2, T3, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3, T4>(this IContainer injectionContainer, T instance, Func<T, Func<T1, T2, T3, T4, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3, T4, T5>(this IContainer injectionContainer, T instance, Func<T, Func<T1, T2, T3, T4, T5, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3, T4, T5, T6>(this IContainer injectionContainer, T instance, Func<T, Func<T1, T2, T3, T4, T5, T6, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(instance, method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult>(this IContainer injectionContainer, Func<T, Func<TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1>(this IContainer injectionContainer, Func<T, Func<T1, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2>(this IContainer injectionContainer, Func<T, Func<T1, T2, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3>(this IContainer injectionContainer, Func<T, Func<T1, T2, T3, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3, T4>(this IContainer injectionContainer, Func<T, Func<T1, T2, T3, T4, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3, T4, T5>(this IContainer injectionContainer, Func<T, Func<T1, T2, T3, T4, T5, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public static TResult Invoke<T, TResult, T1, T2, T3, T4, T5, T6>(this IContainer injectionContainer, Func<T, Func<T1, T2, T3, T4, T5, T6, TResult>> method)
        {
            return (TResult)injectionContainer.Invoke(injectionContainer.Resolve<T>(), method);
        }
    }
}