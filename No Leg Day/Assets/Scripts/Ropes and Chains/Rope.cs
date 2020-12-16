using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rope : Chain
{
    [Tooltip("The line renderer will define how the rope looks")]
    [SerializeField] protected LineRenderer LineRenderer = null;

    [HideInInspector]
    [SerializeField] protected Transform[] Links = null;

    public override void Rebuild()
    {
        base.Rebuild();
        Links = GetLinks();
        LineRenderer.positionCount = Links.Length + 1;
        RenderRope();
    }

    protected Transform[] GetLinks()
    {
        Transform parentTransform = ChainParent.transform;
        Transform[] Links = new Transform[parentTransform.childCount];
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Links[i] = parentTransform.GetChild(i);
        }
        return Links;
    }

    protected void LateUpdate()
    {
        RenderRope();
    }

    protected void RenderRope()
    {
        for (int i = 0; i < Links.Length; i++)
        {
            LineRenderer.SetPosition(i, Links[i].position + Links[i].up * YSize * 0.5f);
        }
        LineRenderer.SetPosition(LineRenderer.positionCount - 1, Links[Links.Length - 1].position - Links[Links.Length - 1].up * YSize * 0.5f);
    }
}
