public interface IConsumer<TConsumable> where TConsumable : IConsumable
{
    public void Consume(TConsumable consumable);
}