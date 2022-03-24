using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

public class AIHelperIdleState : State<EState, ETransition>
{
    protected override EState GetStateID()
    {
        return EState.Idle;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();


    }


}