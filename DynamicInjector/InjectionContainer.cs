using System;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicInjector
{
    public class InjectionContainer
    {
        private readonly Dictionary<string, object> globalServices = new Dictionary<string, object>();
        private readonly Dictionary<string, Type> localServices = new Dictionary<string, Type>();

        public void RegisterGlobal<TService>(string? name = null)
        {
            globalServices.Add(name ?? typeof(TService).FullName, Resolve<TService>()!);
        }

        public void RegisterGlobal<TService, TImplementation>(string? name = null) where TImplementation : TService
        {
            globalServices.Add(name ?? typeof(TService).FullName, Resolve<TImplementation>()!);
        }

        public void RegisterGlobal<TService, TImplementation>(TImplementation implementation, string? name = null) where TImplementation : TService
        {
            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            globalServices.Add(name ?? typeof(TService).FullName, implementation);
        }

        public void RegisterLocal<TService>(string? name = null)
        {
            localServices.Add(name ?? typeof(TService).FullName, typeof(TService));
        }

        public void RegisterLocal<TService, TImplementation>(string? name = null) where TImplementation : TService
        {
            localServices.Add(name ?? typeof(TService).FullName, typeof(TService));
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            ConstructorInfo[]? constructorInfos = type.GetConstructors();

            if (constructorInfos.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            if (constructorInfos.Length > 1)
            {
                throw new InvalidOperationException($"Service \"{type.FullName}\" contains multiple constructors!");
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
                if (CheckIfServiceExists(parameterInfo.Name, out object? serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else if (CheckIfServiceExists(parameterInfo.ParameterType.FullName, out serviceInstance))
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

                if (CheckIfServiceExists(propertyInfo.Name, out object? serviceInstance))
                {
                    propertyInfo.SetValue(instance, serviceInstance);
                }
                else if (CheckIfServiceExists(propertyInfo.PropertyType.FullName, out serviceInstance))
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

        public object Invoke<TInstance>(TInstance instance, Func<TInstance, Delegate> method)
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
                if (CheckIfServiceExists(parameterInfo.Name, out object? serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else if (CheckIfServiceExists(parameterInfo.ParameterType.FullName, out serviceInstance))
                {
                    arguments.Add(serviceInstance!);
                }
                else
                {
                    throw new InvalidOperationException($"Service \"{parameterInfo.ParameterType.FullName}\" not found!");
                }
            }

            return delegateMethod.DynamicInvoke(arguments.ToArray());
        }

        private bool CheckIfServiceExists(string typeName, out object? serviceInstance)
        {
            foreach (KeyValuePair<string, object> serviceType in globalServices)
            {
                if (typeName == serviceType.Key)
                {
                    serviceInstance = globalServices[serviceType.Key];
                    return true;
                }
            }

            foreach (KeyValuePair<string, Type> serviceType in localServices)
            {
                if (typeName == serviceType.Key)
                {
                    serviceInstance = Resolve(localServices[serviceType.Key]);
                    return true;
                }
            }

            serviceInstance = null;
            return false;
        }
    }
}