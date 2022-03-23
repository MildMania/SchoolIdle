public class AIHelperFSMController : FSMController<AIHelperFSMController.EState,
    AIHelperFSMController.ETransition>
{
    public enum EState
    {
        None = 0,
        Idle = 1,
        Store = 2,
        Drop = 3
    }

    public enum ETransition
    {
        None = 0,
        Idle = 1,
        Store = 2,
        Drop = 3
    }
}