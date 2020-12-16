using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [HideInInspector] public Transform PlayerTransform;
    public Vector3 Offset;
    public float speed;

    [SerializeField] private float minXPos = 0;
    [SerializeField] private float maxXPos = 0;

    private bool hasChangedOffsetTemporarily = false;

    public static CameraScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Temporarily changes the camera's offset from target (Use ResetOffset to change it back).
    /// </summary>
    /// <param name="newOffset"></param>
    public void ChangeOffsetTemporarily(Vector3 newOffset)
    {
        hasChangedOffsetTemporarily = true;
        StartCoroutine(MoveOffsetTemporarily(newOffset));
    }

    private IEnumerator MoveOffsetTemporarily(Vector3 newOffset)
    {
        Vector3 originalOffset = Offset;
        Offset = newOffset;
        yield return new WaitUntil(() => !hasChangedOffsetTemporarily);
        Offset = originalOffset;
    }

    /// <summary>
    /// Resets camera's offset from target.
    /// </summary>
    public void ResetOffset()
    {
        hasChangedOffsetTemporarily = false;
    }

    private void LateUpdate()
    {
        Vector3 desiredPos = Vector3.MoveTowards(transform.position, PlayerTransform.position + Offset, speed);
        transform.position = desiredPos;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPos, maxXPos),
                                            transform.position.y,
                                            transform.position.z);
    }
}
