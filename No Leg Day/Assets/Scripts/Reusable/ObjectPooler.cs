using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int numOfPooledObjects = 0;
    [SerializeField] private GameObject pooledObject = null;
    [SerializeField] private bool canGrow = false;
    private List<GameObject> gameObjects;

    private void Awake()
    {
        gameObjects = new List<GameObject>();
        for(int i=0; i<numOfPooledObjects; i++)
        {
            SpawnObject();
        }
    }

    public GameObject GetObject()
    {
        GameObject go = gameObjects.Find(g => g.activeSelf == false);
        if(go != null)
        {
            go.SetActive(true);
        }
        else if (canGrow)
        {
            go = SpawnObject();
            numOfPooledObjects++;
            go.SetActive(true);
        }
        return go;
    }

    private GameObject SpawnObject()
    {
        GameObject go = Instantiate(pooledObject);
        go.transform.position = transform.position;
        go.SetActive(false);
        gameObjects.Add(go);
        return go;
    }
}
