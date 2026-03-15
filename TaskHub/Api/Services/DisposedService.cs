namespace Api.Services;

public abstract class DisposedService : IHasInstanceId, IDisposable
{
    public Guid InstanceId { get; } = Guid.NewGuid();
    private readonly string _serviceName;

    protected DisposedService(string serviceName)
    {
        _serviceName = serviceName;
        Console.WriteLine($"[CREATE] {_serviceName}: {InstanceId}");
    }

    public void Dispose()
    {
        Console.WriteLine($"[DISPOSE] {_serviceName}: {InstanceId}");
    }
}