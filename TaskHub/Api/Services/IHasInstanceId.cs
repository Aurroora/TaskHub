namespace Api.Services;

public interface IHasInstanceId
{
    Guid InstanceId { get; }
}