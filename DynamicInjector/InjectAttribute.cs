using System;

namespace DynamicInjector
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}