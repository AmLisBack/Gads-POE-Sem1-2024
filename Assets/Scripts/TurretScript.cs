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
    private float fireTimer = 10f;
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
        //Debug.Log(target);
        if (target == null)
        {
            FindNewTarget();
        }
        if (target != null)
        {
            AimAtTarget();
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                animator.SetTrigger("PlayAnimation");
                Shoot();
                fireTimer = RPM;
            }
            animator.SetTrigger("Idle");

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
        //Rotation the turret towards the enemy it is locked in on
        Vector3 direction = (target.position - firePoint.transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        
        //Moves the drawline to follow the rotation of the object
        Vector3 lineStartOffset = new Vector3(-2.5f, 5f, 0f);
        Vector3 startPosition = transform.position + transform.rotation * lineStartOffset;
        Vector3 endPosition = target.position + new Vector3(0f, 5f, 0f);
        //Comment out the drawline if we dont want it 
        Debug.DrawLine(startPosition, endPosition, Color.blue);
    }
    private void Shoot()
    {
        Vector3 lineStartOffset = new Vector3(-2.5f, 5f, 0f);
        Vector3 startPosition = transform.position + transform.rotation * lineStartOffset;

        GameObject projectile = Instantiate(projectilePrefab, startPosition, firePoint.rotation);
        Net net = projectile.GetComponent<Net>();
        if (net != null)
        {
            net.Seek(target);
        }



    }
}
