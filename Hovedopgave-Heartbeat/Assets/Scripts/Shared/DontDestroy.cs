using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject instance;
    
    private void Awake()
    {
        

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
