using UnityEngine;
using UnityWeld.Binding;
using MMUtils = MMFramework.Utilities.Utilities;

[Binding]
public class PathProgressionFillBarWidget : FillBarWidget
{
    public override float FillBarSize => 1;

    private Vector3 _initialCharacterPos;

    protected override void AwakeCustomActions()
    {
        RegisterToCharacterFSM();

        base.AwakeCustomActions();
    }

    protected override void OnDestroyCustomActions()
    {
        UnregisterFromCharacterFSM();

        base.OnDestroyCustomActions();
    }

    protected override void ActivatingCustomActions()
    {
        // _initialCharacterPos = Character.Instance.transform.position;

        TryUpdateBar(0);

        base.ActivatingCustomActions();
    }

    private void RegisterToCharacterFSM()
    {
        // CharacterFSM charFSM = Character.Instance.CharacterFSM;
        //
        // CharacterRunState runState = charFSM.GetState<CharacterRunState>();
        //
        // runState.OnCharacterMoved += OnCharacterMoved;
    }

    private void UnregisterFromCharacterFSM()
    {
        // if (Character.Instance == null)
        // {
        //     return;
        // }

        // CharacterFSM charFSM = Character.Instance.GetComponentInChildren<CharacterFSM>();
        //
        // CharacterRunState runState = charFSM.GetState<CharacterRunState>();
        //
        // runState.OnCharacterMoved -= OnCharacterMoved;
    }

    private void OnCharacterMoved(Vector3 position)
    {
        float initialZ = _initialCharacterPos.z;
        float finishLineZ = Finishline.Instance.transform.position.z;

        float normVal = MMUtils.Normalize(position.z, finishLineZ,
            initialZ, 1, 0);

        TryUpdateBar(normVal);
    }

    private bool TryUpdateBar(float normVal)
    {
        TargetNormalizedValue = normVal;

        return true;
    }
}