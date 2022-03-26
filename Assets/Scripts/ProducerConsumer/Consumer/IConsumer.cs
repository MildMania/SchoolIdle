public interface IConsumer<TResource> where TResource : IResource
{
    public void Consume(TResource resource);
}