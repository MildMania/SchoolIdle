using UnityEngine;


public class CharacterAnimationController : MonoBehaviour
{
	[SerializeField] private Animator _animator = null;

	[MMSerializedInterface(typeof(IMovementExecutor))] [SerializeField]
	private Component _movementExecutor = null;

	public IMovementExecutor MovementExecutor => _movementExecutor as IMovementExecutor;

	[SerializeField] private float _movementAnimDamping = 0.05f;

	private static readonly int MovementSpeed
		= Animator.StringToHash(MOVEMENT_SPEED);

	private const string MOVEMENT_SPEED = "MovementSpeed";

	private void Update()
	{
		UpdateAnimator();
	}

	private void UpdateAnimator()
	{
		_animator.SetFloat(
			MovementSpeed,
			MovementExecutor.MovementSpeed,
			_movementAnimDamping,
			Time.deltaTime);
	}
}