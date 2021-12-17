namespace DynamicInjector_Test;

internal class FirstTestService : IFirstTestService
{
    private int count = 0;

    public void Print()
    {
        Console.WriteLine(nameof(FirstTestService) + " | " + count++);
    }
}