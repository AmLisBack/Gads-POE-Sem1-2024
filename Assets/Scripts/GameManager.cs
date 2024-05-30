using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text goldAmountText;
    public int gold = 100;

    private float timer = 0f;
    public float delayAmount = 10f;

    public Buildings[] buildings;
    public int repairCost = 20;

    public SniperTower sniperTower;
    public TurretScript turretScript;

    public static GameManager Instance { get; set; }
    void Update()
    {
        Instance = this;
        UpdateGoldText();
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            gold++;
            Debug.Log(gold);
        }
    }

    public void UpdateGoldText()
    {
        if (goldAmountText != null)
        {
            goldAmountText.text = gold.ToString();
        }
    }
    public void OnRepairButtonClick(Buildings buildingToRepair)
    {
        Debug.Log("repair clcked");
        if (gold >= repairCost && !buildingToRepair.IsRepaired())
        {
            gold -= repairCost;
            buildingToRepair.Repair();
            ReplaceBuildingPrefab(buildingToRepair);
            UpdateGoldText();
        }
        else
        {
            //Not enough gold or building already repaired
            Debug.Log("Cannot repair. Insufficient gold or building is already repaired.");
        }
    }
    private void ReplaceBuildingPrefab(Buildings building)
    {
        GameObject repairedPrefab = building.repairedPrefab;

        if (repairedPrefab != null)
        {

            GameObject newBuilding = Instantiate(repairedPrefab, building.transform.position, repairedPrefab.transform.rotation);

            Destroy(building.gameObject);
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i] == building)
                {
                    buildings[i] = newBuilding.GetComponent<Buildings>();
                    break;
                }
            }
        }
    }
    public bool CanAffordUpgrade(int cost)
    {
        return gold >= cost;
    }
    public void OnUpgradeSniperTowerButtonClick(SniperTower towerToUpgrade)
    {
        Debug.Log("upgrade clcked");
        if (towerToUpgrade != null)
        {
            towerToUpgrade.Upgrade();
        }
        else
        {
            Debug.LogError("No SniperTower assigned to the button!");
        }
    }
    public void OnUpgradeTurretUpgradeClick(TurretScript turretToUpgrade)
    {
        Debug.Log("upgrade clcked");
        if (turretToUpgrade != null)
        {
            turretToUpgrade.Upgrade();
        }
        else
        {
            Debug.LogError("No turret assigned to the button!");
        }
    }
    public void IncreaseOnDeath(int goldToIncrease)
    {
        this.gold += goldToIncrease;
    }
}
