using System;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicInjector
{
    /// <summary>
    /// Container which handles all registered services
    /// </summary>
    public class InjectionContainer : IContainer
    {
        private readonly Dictionary<string, object> globalServices = new Dictionary<string, object>();
        private readonly Dictionary<string, Type> localServices = new Dictionary<string, Type>();
        private readonly Dictionary<string, Type> scopedServices = new Dictionary<string, Type>();

        /// <summary>
        /// Registers a global service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="name"></param>
        public void RegisterGlobal<TService>(string? name = null)
        {
            globalServices.Add(name ?? typeof(TService).FullName, Resolve<TService>()!);
        }

        /// <summary>
        /// Registers a global service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="name"></param>
        public void RegisterGlobal<TService, TImplementation>(string? name = null) where TImplementation : TService
        {
            globalServices.Add(name ?? typeof(TService).FullName, Resolve<TImplementation>()!);
        }

        /// <summary>
        /// Registers a global service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="implementation"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterGlobal<TService, TImplementation>(TImplementation implementation, string? name = null) where TImplementation : TService
        {
            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            globalServices.Add(name ?? typeof(TService).FullName, implementation);
        }

        /// <summary>
        /// Registers a local service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="name"></param>
        public void RegisterLocal<TService>(string? name = null)
        {
            localServices.Add(name ?? typeof(TService).FullName, typeof(TService));
        }

        /// <summary>
        /// Registers a local service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="name"></param>
        public void RegisterLocal<TService, TImplementation>(string? name = null) where TImplementation : TService
        {
            localServices.Add(name ?? typeof(TService).FullName, typeof(TImplementation));
        }

        /// <summary>
        /// Registers a scoped service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="name"></param>
        public void RegisterScoped<TService>(string? name = null)
        {
            scopedServices.Add(name ?? typeof(TService).FullName, typeof(TService));
        }

        /// <summary>
        /// Registers a scoped service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="name"></param>
        public void RegisterScoped<TService, TImplementation>(string? name = null) where TImplementation : TService
        {
            scopedServices.Add(name ?? typeof(TService).FullName, typeof(TImplementation));
        }

        /// <summary>
        /// Begins a new scope
        /// </summary>
        /// <returns>A new scope</returns>
        public LifetimeScope BeginScope()
        {
            return new LifetimeScope(this);
        }

        /// <inheritdoc cref="IContainer.Resolve{T}"/>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <inheritdoc cref="IContainer.Resolve(Type)"/>
        public object Resolve(Type type)
        {
            return InternalResolve(type, null);
        }

        internal object InternalResolve(Type type, LifetimeScope? scope)
        {
            ConstructorInfo[]? constructorInfos = type.GetConstructors();

            if (constructorInfos.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            if (constructorInfos.Length > 1)
            {
                throw new InvalidOperationException($"Service \"{type.FullName}\" cannot contain multiple constructors!");
            }

            ConstructorInfo constructorInfo = constructorInfos[0];
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (parameterInfos.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            List<object> arguments = new List<object>();

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                if (CheckIfServiceExists(parameterInfo.Name, scope, out object? serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else if (CheckIfServiceExists(parameterInfo.ParameterType.FullName, scope, out serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else
                {
                    throw new InvalidOperationException($"Service \"{parameterInfo.ParameterType.FullName}\" not found!");
                }
            }

            object instance = Activator.CreateInstance(type, arguments.ToArray());

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetCustomAttribute<InjectAttribute>() == null)
                {
                    continue;
                }

                if (CheckIfServiceExists(propertyInfo.Name, scope, out object? serviceInstance))
                {
                    propertyInfo.SetValue(instance, serviceInstance);
                }
                else if (CheckIfServiceExists(propertyInfo.PropertyType.FullName, scope, out serviceInstance))
                {
                    propertyInfo.SetValue(instance, serviceInstance);
                }
                else
                {
                    throw new InvalidOperationException($"Service \"{propertyInfo.PropertyType.FullName}\" not found!");
                }
            }

            return instance;
        }

        /// <inheritdoc cref="IContainer.Invoke{TInstance}(TInstance, Func{TInstance, Delegate})"/>
        public object Invoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method)
        {
            return InternalInvoke(instance, method, null);
        }

        internal object InternalInvoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method, LifetimeScope? scope)
        {
            Delegate delegateMethod = method.Invoke(instance);
            MethodInfo methodInfo = delegateMethod.Method;
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            if (parameterInfos.Length == 0)
            {
                return delegateMethod.DynamicInvoke();
            }

            List<object> arguments = new List<object>();

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                if (CheckIfServiceExists(parameterInfo.Name, scope, out object? serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else if (CheckIfServiceExists(parameterInfo.ParameterType.FullName, scope, out serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else
                {
                    throw new InvalidOperationException($"Cannot find service \"{parameterInfo.ParameterType.FullName}\"!");
                }
            }

            return delegateMethod.DynamicInvoke(arguments.ToArray());
        }

        private bool CheckIfServiceExists(string typeName, LifetimeScope? scope, out object? serviceInstance)
        {
            foreach (KeyValuePair<string, Type> serviceType in localServices)
            {
                if (typeName == serviceType.Key)
                {
                    serviceInstance = InternalResolve(localServices[serviceType.Key], scope);
                    return true;
                }
            }

            foreach (KeyValuePair<string, object> serviceType in globalServices)
            {
                if (typeName == serviceType.Key)
                {
                    serviceInstance = globalServices[serviceType.Key];
                    return true;
                }
            }

            if (scope is null)
            {
                serviceInstance = null;
                return false;
            }

            foreach (KeyValuePair<string, Type> serviceType in scopedServices)
            {
                if (typeName == serviceType.Key)
                {
                    if (scope.Services.ContainsKey(serviceType.Key))
                    {
                        serviceInstance = scope.Services[serviceType.Key];
                    }
                    else
                    {
                        serviceInstance = InternalResolve(scopedServices[serviceType.Key], scope);
                        scope.Services.Add(serviceType.Key, serviceInstance);
                    }
                    return true;
                }
            }

            serviceInstance = null;
            return false;
        }
    }
}