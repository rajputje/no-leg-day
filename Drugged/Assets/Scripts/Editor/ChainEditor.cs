using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Chain),true)]
public class ChainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            GameObject[] chains = Selection.gameObjects;
            foreach(GameObject chain in chains)
            {
                chain.GetComponent<Chain>().Rebuild();
            }
        }
    }
}
