using UnityEngine;

public interface ICollisionReceiver
{
    void OnTriggerEnter(Collider other);
    void OnCollisionEnter(Collision collision);

    // void OnTriggerExit(Collider other);
    // void OnCollisionExit(Collision collision);
}
