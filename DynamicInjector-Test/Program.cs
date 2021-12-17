using DynamicInjector;

namespace DynamicInjector_Test;

internal class Prgramm
{
    private static void Main()
    {
        InjectionContainer injector = new InjectionContainer();
        injector.RegisterGlobal<IFirstTestService, FirstTestService>();
        injector.RegisterLocal<SecondTestService>();

        TestClass? testClass = injector.Resolve<TestClass>();

        testClass.Test();

        injector.Invoke<TestClass>(testClass, (c) => c.Test);
    }
}