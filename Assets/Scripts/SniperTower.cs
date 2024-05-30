using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SniperTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float RPM = 3f;
    public int upgradeLevel = 0;
    public float rpmIncreasePerLevel = 1;
    private int initialUpgradeCost = 20;  
    private int upgradeCostIncrease = 50;
    public TMP_Text upgradeCostText;

    private Transform target;
    private float fireTimer = 0f;
    
    public Buildings buildingScript;
    
    // Start is called before the first frame update
    void Start()
    {
        buildingScript = GetComponent<Buildings>();
        if (buildingScript == null)
        {
            Debug.LogError("SniperTower is missing a Buildings component!");
            return;
        }

       
        if (upgradeCostText != null)
        {
            upgradeCostText.text = "Upgrade Cost: " + initialUpgradeCost.ToString();
        }

        buildingScript.currentRPM = RPM;
    }

    // Update is called once per frame
    void Update()
    {
        
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = 60f / buildingScript.currentRPM; 
        }
       // Debug.Log(target);
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
    public void Upgrade()
    {
        Debug.Log("upgraded bvuilding");
        int currentUpgradeCost = initialUpgradeCost + (upgradeLevel * upgradeCostIncrease);
        if (upgradeCostText != null)
        {
            upgradeCostText.text =  currentUpgradeCost.ToString();
        }

        if (GameManager.Instance.CanAffordUpgrade(currentUpgradeCost))
        {
            upgradeLevel++; 
            GameManager.Instance.gold -= currentUpgradeCost;
            GameManager.Instance.UpdateGoldText();

            
            float rpmIncrease = RPM * rpmIncreasePerLevel * upgradeLevel;  
            buildingScript.currentRPM = RPM + rpmIncrease;

           
            if (upgradeCostText != null)
            {
                upgradeCostText.text = (currentUpgradeCost + upgradeCostIncrease).ToString();
            }

            Debug.Log("Sniper Tower upgraded to level " + upgradeLevel + ", RPM: " + buildingScript.currentRPM);
        }
        else
        {
            Debug.Log("Not enough gold to upgrade!");
        }
    }

}


