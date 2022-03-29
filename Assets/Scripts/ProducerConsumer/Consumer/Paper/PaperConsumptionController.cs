public class PaperConsumptionController : ConsumptionController<PaperConsumer, Paper>
{
    void Awake()
    {
        ConsumerProvider.Instance.AddConsumer(_consumer, typeof(Paper));
    }
}