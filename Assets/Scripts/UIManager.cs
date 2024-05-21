using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Reference to the UI panel that should be displayed
    public GameObject UIPanel;

    void Start()
    {
        // Ensure the UI panel is initially hidden
        if (UIPanel != null)
        {
            UIPanel.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        // Show the UI panel when the object is clicked
        if (UIPanel == null)
        {
            UIPanel.SetActive(true);
        }
    }
}

