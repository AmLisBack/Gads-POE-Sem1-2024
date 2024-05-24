using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    // Start is called before the first frame update


    void Update()
    {

        Vector3 dir = target.position + new Vector3(0f, 5f, 0f) - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
}
