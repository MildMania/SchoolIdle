public class FolderConsumptionController : ConsumptionController<FolderConsumer,Folder>
{
    void Awake()
    {
        ConsumerProvider.Instance.AddConsumer(_consumer, typeof(Folder));
    }
}