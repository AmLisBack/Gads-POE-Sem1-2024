using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectilePrefab;
    public Transform firePoint;
    
    public float RPM = 10f;

    private Transform target;
    private float fireTimer = 0f;
    public float displayFireTimer;
    private Animator animator;

    // Update is called once per frame
    private void Start()
    {
        
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        displayFireTimer = fireTimer;
        Debug.Log(target);
        if (target == null)
        {
            FindNewTarget();
        }
        if (target != null)
        {
            AimAtTarget();
            fireTimer -= Time.deltaTime;
            animator.Play("Idle");
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = RPM;
            }

        }
    }

    public void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
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
        Debug.DrawLine(firePoint.transform.position, target.position + new Vector3(0f, 5f, 0), Color.blue);
        Vector3 direction = (target.position - firePoint.transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    private void Shoot()
    {
        animator.Play("Fire");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Net net = projectile.GetComponent<Net>();
        if (net != null)
        {
            net.Seek(target);
        }



    }
}
