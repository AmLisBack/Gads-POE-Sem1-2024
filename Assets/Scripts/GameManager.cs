using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public TMP_Text goldAmountText; 
  public int gold = 10;

  private float timer = 0f;
  public float delayAmount = 10f;
  
  public Buildings[] buildings; 
  public int repairCost = 20;

  void Update()
  {
    UpdateGoldText();
    timer += Time.deltaTime;

    if (timer >= delayAmount)
    {
      timer = 0f;
      gold++;
      Debug.Log(gold);
    }
  }
  private void UpdateGoldText()
  {
    if (goldAmountText != null)
    {
      goldAmountText.text = gold.ToString();
    }
  }
  public void OnRepairButtonClick(Buildings buildingToRepair)
  {
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
      GameObject newBuilding = Instantiate(repairedPrefab, building.transform.position, building.transform.rotation);
      Destroy(building.gameObject); // Remove the old building
      
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
  
}
