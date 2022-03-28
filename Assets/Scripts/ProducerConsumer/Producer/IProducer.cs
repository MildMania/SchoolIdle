public interface IProducer<TResource> where TResource : IResource
{
    public void Produce(TResource resource);
}