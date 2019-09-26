using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour
{

    public Vector2 minusPosition;
    public GameObject minusSignObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject addItem(GameObject obj)
    {
        GameObject item = (GameObject)Instantiate(obj, transform);
        return item;
    }

    public GameObject addCartItem(GameObject obj)
    {
        GameObject mainIcon = (GameObject)Instantiate(obj, transform);
        GameObject minus = (GameObject)Instantiate(minusSignObject, transform.position, Quaternion.identity, mainIcon.transform);
        minus.transform.localPosition = minusPosition;

        return mainIcon;
    }

    public void removeItem(GameObject obj)
    {
        Destroy(obj);
    }

}
