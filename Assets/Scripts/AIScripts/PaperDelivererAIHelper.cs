using Sirenix.OdinInspector;
using UnityEngine;


public class PaperDelivererAIHelper : BaseAIHelper<PaperUnloadBehaviour, PaperLoadBehaviour, PaperConsumer, PaperProducer, Paper>
{
    public PaperLoadBehaviour PaperLoadBehaviour;
    public PaperUnloadBehaviour PaperUnloadBehaviour;
}