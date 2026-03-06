namespace Api.Services;

public static class ServiceProviderExtensions
{
    public static void PrintServiceDifference<T>(this IServiceProvider services, string scopeName) where T : class, IHasInstanceId
    {
        Console.WriteLine($"\n=== {scopeName} - {typeof(T).Name} ===");

        // Резолвим первый раз
        var first = services.GetService<T>();
        Console.WriteLine($"First: {first?.InstanceId}");

        // Резолвим второй раз
        var second = services.GetService<T>();
        Console.WriteLine($"Second: {second?.InstanceId}");

        // Проверяем, одинаковые ли это объекты
        if (first == second)
        {
            Console.WriteLine($"✓ Same instance (ReferenceEqual)");
        }
        else
        {
            Console.WriteLine($"✗ Different instances");
        }
    }
}