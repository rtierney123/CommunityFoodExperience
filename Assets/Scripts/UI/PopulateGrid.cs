using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour
{
    public GameObject foodObject;
    public int numberToCreate;

    // Start is called before the first frame update
    void Start()
    {
        populateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void populateGrid()
    {
        GameObject newObject;
        for(int i = 0; i < numberToCreate; i++)
        {
            newObject = (GameObject)Instantiate(foodObject, transform);
        }
    }
}
