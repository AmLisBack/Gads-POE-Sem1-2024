using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float RPM = 3f;

    private Transform target;
    private float fireTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(target);
        if(target == null)
        {
            FindNewTarget();
        }
        if(target != null)
        {
            AimAtTarget();
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = RPM;
            }

        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); 
        Dart dart = projectile.GetComponent<Dart>();
        if(dart != null)
        {
            dart.Seek(target);
        }
        
         
        
    }
    private void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null) 
        {
            target = nearestEnemy.transform;
        }
    }
    private void AimAtTarget()
    {
        Debug.DrawLine(firePoint.position, target.position +  new Vector3(0f,5f,0), Color.red);

        //Rotates the tower to look towards the target (possibly remove)
        /*
        Vector3 direction = target.position - firePoint.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime*5).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        */

    }

}