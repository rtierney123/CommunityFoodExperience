using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRotatingChild : MonoBehaviour
{
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
