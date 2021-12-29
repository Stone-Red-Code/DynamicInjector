using DynamicInjector;

namespace DynamicInjector_Test;

internal class Prgramm
{
    private static void Main()
    {
        InjectionContainer container = new InjectionContainer();
        container.RegisterScoped<IFirstTestService, FirstTestService>();
        container.RegisterLocal<SecondTestService>();
        container.RegisterGlobal<IFirstTestService, FirstTestService>();

        using (LifetimeScope scope = container.BeginScope())
        {
            TestClass? testClass = scope.Resolve<TestClass>();
            testClass.Test();
            int res = scope.Invoke<TestClass, int, SecondTestService>(testClass, (i) => i.SecondTest);

            Console.WriteLine(res);
            scope.Invoke(testClass, (i) => i.SecondTest);
            scope.Invoke(testClass, (i) => i.SecondTest);
        }

        Console.WriteLine();

        using (LifetimeScope scope = container.BeginScope())
        {
            scope.Resolve<TestClass>().Test();
            scope.Resolve<TestClass>().Test();
            scope.Resolve<TestClass>().Test();
            scope.Resolve<TestClass>().Test();
        }
    }
}