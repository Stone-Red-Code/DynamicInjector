using DynamicInjector;

namespace DynamicInjector_Test;

internal class TestClass
{
    private readonly IFirstTestService testService;

    [Inject]
    private SecondTestService? SecondTestService { get; set; }

    public TestClass(IFirstTestService yes)
    {
        testService = yes;
    }

    public void Test()
    {
        testService.Print();
        SecondTestService?.Output();
    }

    public int SecondTest(SecondTestService secondTestService)
    {
        secondTestService.Output();
        return 100;
    }
}