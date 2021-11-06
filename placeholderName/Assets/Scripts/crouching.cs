using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouching : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("s"))
        {
            transform.localScale = new Vector3 (1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3 (1, 2, 1);
        }
    }
}
