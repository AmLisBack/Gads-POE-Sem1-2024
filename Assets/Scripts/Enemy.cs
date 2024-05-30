using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public double enemyHealth = 10;
    public float damage = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthCheck();
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, 0.05f);
    }
    public void HealthCheck()
    {

        if (enemyHealth <= 0)
        {
           
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Net"))
        {
            Destroy(other.gameObject);
            enemyHealth -= 10;
        }
        if(other.CompareTag("Dart"))
        {
            enemyHealth -= 3.5;
            Destroy(other.gameObject);
        }
    }
}
