using Sirenix.OdinInspector;
using UnityEngine;


public class FolderDelivererAIHelper : BaseAIHelper<FolderUnloadBehaviour, FolderLoadBehaviour, FolderConsumer, FolderProducer, Folder>
{
    public FolderLoadBehaviour FolderLoadBehaviour;
    public FolderUnloadBehaviour FolderUnloadBehaviour;
}
