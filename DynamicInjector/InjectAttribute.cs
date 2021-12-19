using System;

namespace DynamicInjector
{
    /// <summary>
    /// Indicates that the associated property should have a value injected from the service container during initialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}