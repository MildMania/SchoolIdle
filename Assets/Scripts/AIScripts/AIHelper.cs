using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using MMFramework.Tasks.Examples;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class AIHelper : SerializedMonoBehaviour
{
    [OdinSerialize] private IResource _resource;

    [OdinSerialize] private Dictionary<IResource, GameObject> _behaviourCarrierObjects;

    [OdinSerialize] private Dictionary<IResource, string> _loadBehaviourTypes;

    private BaseLoadBehaviour<BaseProducer<IResource>, IResource> _currentLoadBehaviour;
    private BaseUnloadBehaviour<BaseConsumer<IResource>, IResource> _currentUnloadBehaviour;

    private void Awake()
    {
        foreach (var item in _behaviourCarrierObjects)
        {
            GameObject obj = item.Value;

            if (!item.Key.Equals(_resource))
                obj.SetActive(false);
            else
            {
                string compName = _loadBehaviourTypes[_resource];

                //Component component = obj.GetComponent()
                //PaperLoadBehaviour beh = (PaperLoadBehaviour)component;

                System.Type tp;

                _currentLoadBehaviour = obj.GetComponent<BaseLoadBehaviour<BaseProducer<IResource>, IResource>>();
                _currentUnloadBehaviour = obj.GetComponent<BaseUnloadBehaviour<BaseConsumer<IResource>, IResource>>();
            }
        }
    }

    //public List<IAIInteractable> GetConsumers()
    //{
    //    return ConsumerProvider.Instance.GetConsumers(_resource);
    //}

    //public List<IAIInteractable> GetProducers()
    //{
    //    return ProducerProvider.Instance.GetProducers(_resource);
    //}
}
