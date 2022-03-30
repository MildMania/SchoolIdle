using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

public class AIHelperFolderDeliverState : AIHelperDeliverState
{
    [SerializeField] private FolderDelivererAIHelper _folderDelivererAiHelper;

    protected override List<IAIInteractable> GetConsumers()
    {
        return ConsumerProvider.Instance.GetConsumers(typeof(Folder));
    }

    protected override void OnDeliverStateCustomActions()
    {
        _folderDelivererAiHelper.FolderLoadBehaviour.OnCapacityEmpty += OnCapacityEmpty;
    }

    protected override void OnExitCustomActions()
    {
        _folderDelivererAiHelper.FolderLoadBehaviour.OnCapacityEmpty -= OnCapacityEmpty;
    }

    private void OnCapacityEmpty()
    {
        FSM.SetTransition(AIHelperFSMController.ETransition.Store);
    }
}
