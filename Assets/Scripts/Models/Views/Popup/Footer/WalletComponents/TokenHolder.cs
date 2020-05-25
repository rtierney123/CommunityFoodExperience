using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenHolder : MonoBehaviour
{
    public GameObject tokenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clearTokens()
    {
        foreach(Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void addToken()
    {
        GameObject token = Instantiate<GameObject>(tokenPrefab, transform);
    }
}
