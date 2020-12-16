using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupShifterVertical : MonoBehaviour
{
    [SerializeField] private VerticallyShiftable[] verticallyShiftables = null;

    public void MoveAllUp()
    {
        foreach(VerticallyShiftable v in verticallyShiftables)
        {
            v.MoveUp();
        }
    }

    public void MoveAllDown()
    {
        foreach(VerticallyShiftable v in verticallyShiftables)
        {
            v.MoveDown();
        }
    }
}
