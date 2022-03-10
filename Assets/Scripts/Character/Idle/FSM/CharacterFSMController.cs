public class CharacterFSMController : FSMController<CharacterFSMController.EState, CharacterFSMController.EState>
{
	public enum EState
	{
		None = 0,
		MovementIdle = 1,
		Walk = 2,
		ActionIdle = 20,
		Attack = 21,
		AttackBlocker = 22,
		Fail = 3,
		Win = 4,
	}
}