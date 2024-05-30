using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rhino : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 scaleToIncrement = new Vector3(0.01f, 0.01f, 0.01f);
    public int health = 3;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    void Start()
    {
        health3.SetActive(true);

        health2.SetActive(true);

        health1.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale += scaleToIncrement * Time.deltaTime;
        switch (health)
        {
            case 2:
                health3.SetActive(false); break;
            case 1:
                health2.SetActive(false); break;
            case 0:
                health1.SetActive(false); break;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= 1;
        }
    }
}
