public class FolderConsumptionController : ConsumptionController<FolderConsumer,Folder>
{
    void OnEnable()
    {
        if (_consumer.IsAiInteractible())
        {
            ConsumerProvider.Instance.AddConsumer(_consumer, typeof(Folder));
        }
    }
}