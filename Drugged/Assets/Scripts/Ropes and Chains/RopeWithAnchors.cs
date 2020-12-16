using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeWithAnchors : Rope
{
    [Tooltip("(Optional) The object to which one end of the rope is attached")]
    [SerializeField] protected GameObject Anchor1;
    
    [Tooltip("Offset from first segment's transform")]
    [SerializeField] protected Vector3 Anchor1Offset;
    
    [Tooltip("(Optional) The object to which the other end of the rope is attached")]
    [SerializeField] protected GameObject Anchor2;

    [Tooltip("Offset from last segment's transform")]
    [SerializeField] protected Vector3 Anchor2Offset;

    public override void Rebuild()
    {
        base.Rebuild();
        if (Anchor1)
        {
            ConnectAnchor1();
        }
        if(Anchor2)
        {
            ConnectAnchor2();
        }
    }
    private void ConnectAnchor1()
    {
        Anchor1.transform.position = Links[0].position + Anchor1Offset;
        Rigidbody2D rb = Anchor1.GetComponent<Rigidbody2D>();
        if (rb)
        {
            Links[0].GetComponent<HingeJoint2D>().connectedBody = rb;
        }
    }

    private void ConnectAnchor2()
    {
        Anchor2.transform.position = Links[Links.Length - 1].position + Anchor2Offset;
        Rigidbody2D rb = Anchor1.GetComponent<Rigidbody2D>();
        Transform lastLink = Links[Links.Length - 1];
        HingeJoint2D hj = lastLink.gameObject.AddComponent<HingeJoint2D>();
        hj.anchor = lastLink.InverseTransformPoint(lastLink.position - transform.up * YSize * 0.5f);
        if (rb)
        {
            hj.connectedBody = rb;
        }
    }
}
