using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private float speed = 40f;
    private Transform target;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }
    // Start is called before the first frame update


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        Vector3 dir = target.position + new Vector3(0f, 5f, 0f) - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = lookRotation;
    }
}
