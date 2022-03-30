using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimationController<TAnimation> : MonoBehaviour where TAnimation : Enum
{
    [Serializable]
    public class AnimationInfo
    {
        [SerializeField] private TAnimation _animationType = default;
        public TAnimation AnimationType => _animationType;

        [SerializeField] private string _animationName = default;
        public string AnimationName => _animationName;
    }

    [SerializeField] private Animator _animator = null;
    [SerializeField] private List<AnimationInfo> _animationInfo = null;
    [HideInInspector] private TAnimation CurrentAnimationType { get; set; }

    public Animator Animator => _animator;


    public void PlayAnimation(TAnimation animationType, float transitionDuration = 0.1f,
        float normalizedAnimTime = 0, float fixedTimeOffset = 0)
    {
        if (CurrentAnimationType.Equals(animationType))
        {
            return;
        }

        string animationName = GetAnimationName(animationType);

        if (animationName == default || _animator == null)
        {
            return;
        }

        CurrentAnimationType = animationType;

        _animator.CrossFadeInFixedTime(animationName, transitionDuration, 0, fixedTimeOffset, normalizedAnimTime);
    }

    private string GetAnimationName(TAnimation animationType)
    {
        foreach (AnimationInfo animationInfo in _animationInfo)
        {
            if (animationInfo.AnimationType.Equals(animationType))
            {
                return animationInfo.AnimationName;
            }
        }

        return default;
    }

    public void StopAnimation()
    {
        _animator.StopPlayback();
    }
}