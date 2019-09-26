using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public Food[] content;
    public Cart cart;
    public PopulateGrid grid;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Food food in content)
        {
            food.setCart(cart);
            grid.addItem(food.getGameObject());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
