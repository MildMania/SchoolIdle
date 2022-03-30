public class PaperConsumptionController : ConsumptionController<PaperConsumer, Paper>
{
    void Awake()
    {
        if (_consumer.IsAiInteractible())
        {
            ConsumerProvider.Instance.AddConsumer(_consumer, typeof(Paper));
        }
    }
}