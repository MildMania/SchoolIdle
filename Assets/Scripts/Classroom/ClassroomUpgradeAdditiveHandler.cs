using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomUpgradeAdditiveHandler : ClassroomUpgradeHandler
{
    public override void UpgradeByLevel(int level)
    {
        foreach (var levelObject in _levelObjects)
        {

            if (levelObject.Key > level)
            {
                break;
            }
            
            foreach (var objectList in levelObject.Value)
            {
                objectList.SetActive(true);
            }
            
        }
    }
}
