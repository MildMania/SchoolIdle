public class PaperConsumptionController : ConsumptionController<PaperConsumer, Paper>
{
    void OnEnable()
    {
        if (_consumer.IsAiInteractible())
        {
            ConsumerProvider.Instance.AddConsumer(_consumer, typeof(Paper));
        }
    }
}