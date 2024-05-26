using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    private bool isRepaired = false; 
    public GameObject repairedPrefab;
    public GameObject uiMenu;
    public float currentRPM;
    

    public bool IsRepaired()
    {
        return isRepaired;
    }

    public void Repair()
    {
        isRepaired = true;
        
        
    }
     
}