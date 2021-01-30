using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBarArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotate(int d)
    {
        this.transform.localEulerAngles=new Vector3(0,0,90*d);
    }
}
