﻿using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FrontEndLoosyCollectCommand", menuName = "ScriptableObjects/FrontEndLoosyCollectCommand",
    order = 1)]
public class FrontEndLoosyCollectCommand : BaseCollectCommand
{
    [SerializeField] public float _smoothTime = 0.1f;

    private Bounds _bounds;

    private Vector3 _velocity = Vector3.zero;

    protected override void ExecuteCustomActions(
        Collectible collectible, Action onCollectCommandExecuted)
    {
        _bounds = collectible.Collider.bounds;
        collectible.Collider.enabled = false;
        CoroutineRunner.Instance.StartCoroutine(MoveRoutine(collectible));
        onCollectCommandExecuted?.Invoke();
    }

    protected override void CalculateNextCollectiblePosition(Collectible collectible)
    {
        ParentTransform = CollectedCollectibles.Count == 0
            ? CollectibleContainerTransform
            : CollectedCollectibles[CollectedCollectibles.Count - 1].transform;
    }

    private IEnumerator MoveRoutine(Collectible collectible)
    {
        var collectibleTransform = collectible.transform;
        collectibleTransform.parent = null;


        while (PhaseTracker.Instance.CurrentPhase is GamePhase)
        {
            var collectiblePosition = collectibleTransform.position;

            collectibleTransform.position = Vector3.SmoothDamp(collectiblePosition,
                ParentTransform.position + Vector3.forward * _bounds.size.z,
                ref _velocity, _smoothTime);

            yield return null;
        }
    }
}