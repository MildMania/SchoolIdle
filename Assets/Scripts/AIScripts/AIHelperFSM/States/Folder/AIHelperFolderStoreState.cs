using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

public class AIHelperFolderStoreState : AIHelperStoreState
{
    [SerializeField] private FolderDelivererAIHelper _folderDelivererAiHelper;

    // Do I need to create a new list every time this method is called?
    protected override List<IAIInteractable> GetProducers()
    {
        return ProducerProvider.Instance.GetProducers(typeof(Folder));
    }

    protected override void OnStoreStateCustomActions()
    {
        _folderDelivererAiHelper.FolderLoadBehaviour.OnCapacityFull += OnCapacityFull;
    }

    protected override void OnExitCustomActions()
    {
        base.OnExitCustomActions();

        _folderDelivererAiHelper.FolderLoadBehaviour.OnCapacityFull -= OnCapacityFull;
    }

    private void OnCapacityFull()
    {
        FSM.SetTransition(ETransition.Deliver);
    }
}
