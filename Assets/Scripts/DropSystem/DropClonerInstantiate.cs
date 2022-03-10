using UnityEngine;

public class DropClonerInstantiate : DropClonerBase
{

    public override Drop CloneDrop(Drop drop)
    {
        GameObject cloneObj = Instantiate(drop.gameObject);

        Drop cloneDrop = cloneObj.GetComponent<Drop>();

        return cloneDrop;
    }
}
