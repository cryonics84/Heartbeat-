using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform sphere;
    public int unitsPerMeter = 10;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos.z = -0.1f;
        startingPos.x = -unitsPerMeter / 2 ;
        startingPos.y = unitsPerMeter / 2;
        sphere.transform.position = startingPos;
    }

    // Update is called once per frame
    void Update()
    {
        startingPos.x = -unitsPerMeter / 2;
        startingPos.y = unitsPerMeter / 2;

        Ray ray = new Ray(transform.position, Vector3.forward);
        //Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            //translate to game space
            Vector3 pos = hit.point;
            pos.x *= unitsPerMeter;
            pos.y *= unitsPerMeter;

            //Set sphere position
            sphere.transform.position = startingPos + pos;
        }

        Debug.DrawRay(transform.position, Vector3.forward);


        
    }

}
