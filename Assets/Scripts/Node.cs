using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject[] Spawnables;
    // Start is called before the first frame update
    void Start()
    {
        int size = Spawnables.Length;
        int spawnIndex = Random.Range(0, size+1);
        Debug.Log(spawnIndex);
        if (spawnIndex == size)
        {
            Destroy(gameObject);
            return;
        }
        GameObject spawn = Instantiate(Spawnables[spawnIndex]);
        spawn.transform.position = transform.position;
        Destroy(gameObject);

    }
}
