using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [Tooltip("The link/segment of the chain that is to be repeated")]
    [SerializeField] protected GameObject Link_Prefab = null;

    [SerializeField] protected float YSize = 0;

    [SerializeField] protected int NumOfLinks = 1;

    [HideInInspector]
    [SerializeField] protected GameObject ChainParent = null;

    protected virtual void OnValidate()
    {
        NumOfLinks = Mathf.Clamp(NumOfLinks, 1, NumOfLinks);
    }

    public virtual void Rebuild()
    {
        DeleteExisting();
        Build(NumOfLinks);
    }

    protected virtual void Build(int numOfLinks)
    {
        ChainParent = new GameObject("Chain");
        ChainParent.transform.parent = transform;
        ChainParent.transform.position = transform.position;
        ChainParent.transform.rotation = transform.rotation;
        GameObject prevLink = Instantiate(Link_Prefab, ChainParent.transform);
        prevLink.transform.position = ChainParent.transform.position;
        for (int i = 1; i < numOfLinks; i++)
        {
            prevLink = AddNewLink(GetNextLinkPosition(prevLink),
                prevLink.GetComponent<Rigidbody2D>());
        }
    }

    protected virtual void DeleteExisting()
    {
        if(ChainParent != null)
        {
            DestroyImmediate(ChainParent);
        }
    }

    protected virtual Vector3 GetNextLinkPosition(GameObject prevLink)
    {
        Transform prevTransform = prevLink.transform;
        return (prevTransform.position - prevLink.transform.up * YSize);
    }

    protected virtual GameObject AddNewLink(Vector3 linkTransformPos, Rigidbody2D connectedRb)
    {
        GameObject child = Instantiate(Link_Prefab, ChainParent.transform);
        ConnectLink(child, linkTransformPos, connectedRb);
        return child;
    }

    protected virtual void ConnectLink(GameObject link, Vector3 moveLinkTo, Rigidbody2D connectedRb)
    {
        link.transform.position = moveLinkTo;
        link.GetComponent<HingeJoint2D>().connectedBody = connectedRb;
    }
}
