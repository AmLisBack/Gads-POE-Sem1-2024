using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Helicopter : MonoBehaviour
{
    public GameObject[] heliWaypoints;
    private bool startPatrol;
    private int patrolIndex;
    public Transform target;
    private float rotationSpeed = 5f;
    void Start()
    {
        startPatrol = false;
        patrolIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, heliWaypoints[patrolIndex].transform.position, 10f * Time.deltaTime);
        target = heliWaypoints[patrolIndex].transform;
        if(!startPatrol)
        {
            if (transform.position == heliWaypoints[patrolIndex].transform.position)
            {
                startPatrol = true;
                patrolIndex++;
            }
        }
        
        if(startPatrol)
        {
            if (transform.position == heliWaypoints[patrolIndex].transform.position)
            {
                patrolIndex++;
            }
            if(patrolIndex ==  heliWaypoints.Length)
            {
                startPatrol = false; // MAKE FALSE TO LOOP !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                patrolIndex = 0; // MAKE THIS 0
            }
        }
        if (target != null)
        {
            // Caution - Quaternion Quarantine - Infectious Brain damage lays below
            /* 
            Vector3 direction = (target.position - target.transform.position).normalized;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            */

        }

    }
}
