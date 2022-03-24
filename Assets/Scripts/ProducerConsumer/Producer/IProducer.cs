public interface IProducer<TProducible> where TProducible : IProducible
{
    public void Produce(TProducible producible);
}