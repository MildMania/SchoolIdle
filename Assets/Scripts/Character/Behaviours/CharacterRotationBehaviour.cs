using System;
using System.Collections;
using UnityEngine;

public class CharacterRotationBehaviour : MonoBehaviour
{
    [SerializeField] private CharacterInputController _characterInputController = null;
    [SerializeField] private Transform _characterHeadTransform = null;
    [SerializeField] private float _maxAngleToTurn = 30.0f;
    [SerializeField] private float _headTurnSpeed = 0.1f;


    private IEnumerator _headAngleChangeRoutine = null;
    private float _angleValue = default;

    private void Awake()
    {
        RegisterToPhaseEvents();
    }


    private void Update()
    {
        Vector3 targetAngle = default;
        targetAngle.y = _angleValue;
        _characterHeadTransform.localRotation = Quaternion.Euler(targetAngle * Time.deltaTime);

        Debug.DrawRay(_characterHeadTransform.position,
            _characterHeadTransform.TransformDirection(Vector3.forward) * 1000, Color.green);
    }

    private void OnDestroy()
    {
        UnregisterFromPhaseEvents();
    }

    private void RegisterToPhaseEvents()
    {
        PhaseActionNode.OnTraverseStarted_Static += OnPhaseStarted;
        PhaseActionNode.OnTraverseFinished_Static += OnPhaseFinished;
    }


    private void UnregisterFromPhaseEvents()
    {
        PhaseActionNode.OnTraverseStarted_Static -= OnPhaseStarted;
        PhaseActionNode.OnTraverseFinished_Static -= OnPhaseFinished;
    }


    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
        {
            return;
        }

        _characterInputController.OnCharacterInputStarted += OnCharacterInputStarted;
        _characterInputController.OnCharacterInputPerformed += OnCharacterInputPerformed;
        _characterInputController.OnCharacterInputCancelled += OnCharacterInputCancelled;
    }

    private void OnPhaseFinished(PhaseBaseNode phase)
    {
        if (!(phase is GamePhase))
        {
            return;
        }

        _characterInputController.OnCharacterInputStarted -= OnCharacterInputStarted;
        _characterInputController.OnCharacterInputPerformed -= OnCharacterInputPerformed;
        _characterInputController.OnCharacterInputCancelled -= OnCharacterInputCancelled;
        // StopCoroutine(_headAngleChangeRoutine);
    }

    private void OnCharacterInputStarted(Vector2 input)
    {
        _characterInputController.OnCharacterInputStarted -= OnCharacterInputStarted;
        // if (_headAngleChangeRoutine != null)
        // {
        //     StopCoroutine(_headAngleChangeRoutine);
        // }
        //
        // _headAngleChangeRoutine = HeadAngleChangeCoroutine();
        // StartCoroutine(_headAngleChangeRoutine);
    }

    private void OnCharacterInputPerformed(Vector2 delta)
    {
        float x = delta.x;
        _angleValue = Mathf.Clamp(x * 360, -30, 30);
    }

    private void OnCharacterInputCancelled(Vector2 input)
    {
        _angleValue = 0.0f;
    }

    private IEnumerator HeadAngleChangeCoroutine()
    {
        Vector3 targetAngle;
        Quaternion currentRotation;
        Quaternion targetRotation;
        float currentVelocity = default;
        float angleDifference;
        currentRotation = _characterHeadTransform.localRotation;
        targetAngle = _characterHeadTransform.localEulerAngles;
        targetAngle.y = _angleValue;

        targetRotation = Quaternion.Euler(targetAngle);
        angleDifference = Quaternion.Angle(currentRotation, targetRotation);
        while (true)
        {
            if (angleDifference > 0.0f)
            {
                float t = Mathf.SmoothDampAngle(angleDifference, 0.0f, ref currentVelocity, _headTurnSpeed);
                t = 1.0f - (t / angleDifference);
                _characterHeadTransform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, t);
            }

            yield return null;
        }
    }
}