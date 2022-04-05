using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyUnloadBehaviour : MonoBehaviour
{

    [SerializeField] private Deliverer _deliverer;
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    
    public int UnloadLastMoney(Transform moveTarget)
    {
        
        int lastResourceIndex = _deliverer.GetLastMoneyResourceIndex<Money>();

        if (lastResourceIndex == -1)
        {
            return lastResourceIndex;
        }

        Money money = (Money) _deliverer.MoneyResources[lastResourceIndex];

        _deliverer.MoneyResources.Remove(money);
       _updatedFormationController.RemoveCustomResourceTransform(lastResourceIndex);


        money.transform.DOJump(moveTarget.position, 2, 1, 0.5f)
            .OnComplete(() =>
        {
            money.gameObject.SetActive(false);
            money.transform.SetParent(null);
        });
        
        return lastResourceIndex;
    }
}