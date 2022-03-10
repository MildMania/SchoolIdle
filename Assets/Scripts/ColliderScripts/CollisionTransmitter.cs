using System.Collections.Generic;
using UnityEngine;

public class CollisionTransmitter : MonoBehaviour
{
    private List<ICollisionReceiver> _receivers 
        = new List<ICollisionReceiver>();

    public void Register(ICollisionReceiver receiver) 
    {
        if (_receivers.Contains(receiver))
            return;

        _receivers.Add(receiver);
    }

    public void Unregister(ICollisionReceiver receiver)
    {
        _receivers.Remove(receiver);
    }

    private void OnTriggerEnter(Collider other)
    {
        List<ICollisionReceiver> receivers = new List<ICollisionReceiver>(_receivers);

        foreach (ICollisionReceiver cr in receivers)
            cr.OnTriggerEnter(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        List<ICollisionReceiver> receivers = new List<ICollisionReceiver>(_receivers);

        foreach (ICollisionReceiver cr in receivers)
            cr.OnCollisionEnter(collision);
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     List<ICollisionReceiver> receivers = new List<ICollisionReceiver>(_receivers);
    //
    //     foreach (ICollisionReceiver cr in receivers)
    //         cr.OnTriggerExit(other);
    // }
    //
    // private void OnCollisionExit(Collision collision)
    // {
    //     List<ICollisionReceiver> receivers = new List<ICollisionReceiver>(_receivers);
    //
    //     foreach (ICollisionReceiver cr in receivers)
    //         cr.OnCollisionExit(collision);
    // }
}
