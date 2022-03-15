public class ConsumerFSMController : FSMController<ConsumerFSMController.EState, ConsumerFSMController.EState>
{
	public enum EState
	{
		Idle = 0,
		Consume = 1,
	}
}