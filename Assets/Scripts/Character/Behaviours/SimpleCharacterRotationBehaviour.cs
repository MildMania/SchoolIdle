using UnityEngine;

public class SimpleCharacterRotationBehaviour : BaseCharacterRotationBehaviour
{
    [SerializeField] private float _maxRotation = 30;
    [SerializeField] private Transform _characterModelTransform = null;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private bool _enableDirectionRay = false;

    private float _width;
    private float _normalizedAngle;

    private void Update()
    {
        float previousRotation = _characterModelTransform.localRotation.y;
        float angle = Mathf.Clamp(_normalizedAngle * _maxRotation,
            -_maxRotation, _maxRotation);

        _characterModelTransform.localRotation =
            Quaternion.Euler(new Vector3(0, previousRotation + angle, 0));

        if (_enableDirectionRay)
        {
            Debug.DrawRay(_characterModelTransform.position,
                _characterModelTransform.TransformDirection(Vector3.forward) * 1000, Color.green);
        }
    }

    private void Awake()
    {
        _width = Mathf.Abs(LevelBoundaryProvider.Instance.GetLeftBoundary().x -
                           LevelBoundaryProvider.Instance.GetRightBoundary().x);
    }

    public override void Rotate(float xSwipeAmount)
    {
        _normalizedAngle = Mathf.Clamp((xSwipeAmount - _characterTransform.localPosition.x) / _width, -1, 1);
    }
}