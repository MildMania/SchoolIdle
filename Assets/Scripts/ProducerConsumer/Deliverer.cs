using UnityEngine;

public class Deliverer : MonoBehaviour
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;

    public UpdatedFormationController FormationController
    {
        get => _updatedFormationController;
        set => _updatedFormationController = value;
    }
}