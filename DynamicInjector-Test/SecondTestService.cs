namespace DynamicInjector_Test;

internal class SecondTestService
{
    private readonly IFirstTestService firstTestService;
    private int count = 0;

    public SecondTestService(IFirstTestService firstTestService)
    {
        this.firstTestService = firstTestService;
    }

    public void Output()
    {
        firstTestService.Print();
        Console.WriteLine(nameof(SecondTestService) + " | " + count++);
    }
}