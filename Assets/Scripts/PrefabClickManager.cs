using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabClickMenu : MonoBehaviour
{
    public GameObject uiMenu; 

    void OnMouseDown()
    {
        uiMenu.SetActive(!uiMenu.activeSelf); //Toggle the menu
    }
}