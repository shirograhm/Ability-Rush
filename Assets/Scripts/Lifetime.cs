using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    private float spawned;
    private const float LIFETIME = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawned = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned + LIFETIME < Time.time) Destroy(gameObject);
    }
}
