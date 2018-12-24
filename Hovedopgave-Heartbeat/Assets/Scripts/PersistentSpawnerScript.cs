using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSpawnerScript : MonoBehaviour
{
    public GameObject persistentObject;

    // Start is called before the first frame update
    void Start()
    {
        if(!GameObject.Find("Persistent Objects"))
        {
            Debug.Log("Spawning persistentObjects");
            Instantiate(persistentObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
