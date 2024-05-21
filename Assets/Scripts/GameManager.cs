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
}
