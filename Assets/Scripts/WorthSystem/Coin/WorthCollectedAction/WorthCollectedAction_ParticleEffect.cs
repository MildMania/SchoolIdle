using UnityEngine;

public class WorthCollectedAction_ParticleEffect : WorthCollectedActionBase
{
	[SerializeField] private ParticleEffectPlayer _particleEffectPlayer = null;

	public override void WorthCollected<TWorth>(
		WorthCollector collector, 
		TWorth worth)
	{
		_particleEffectPlayer.Play();
	}
}