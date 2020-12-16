using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadContainer : MonoBehaviour
{
    private static DontDestroyOnLoadContainer instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
