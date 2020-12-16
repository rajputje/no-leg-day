using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainWithAlternatingLinks : Chain
{
    [SerializeField] protected GameObject Link2_Prefab;
    private bool isCurrentLink2 = false;
    private GameObject CurrentLink_Prefab
    {
        get
        {
            isCurrentLink2 = !isCurrentLink2;
            return isCurrentLink2 ? Link2_Prefab : Link_Prefab;
        }
    }

    protected override void Build(int numOfLinks)
    {
        isCurrentLink2 = false;
        base.Build(numOfLinks);
    }

    protected override GameObject AddNewLink(Vector3 linkTransformPos, Rigidbody2D connectedRb)
    {
        GameObject child = Instantiate(CurrentLink_Prefab, ChainParent.transform);
        ConnectLink(child, linkTransformPos, connectedRb);
        return child;
    }
}
