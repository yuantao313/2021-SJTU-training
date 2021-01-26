using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private bool status;

    // Start is called before the first frame update
    void Start()
    {
        this.status = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getStatus()
    {
        return this.status;
    }

    private void OnCollisionEnter(Collider other)
    {
        if (other.CompareTag("Obstruction"))
        {
            this.status = false;
        }
    }

    private void OnCollisionExit(Collider other)
    {
        if (other.CompareTag("Obstruction"))
        {
            this.status = true;
        }    
    }
}
