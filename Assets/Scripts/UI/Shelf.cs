using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{

    public CanvasController canvasController;
    public Cart cart;
    public PopulateGrid grid;
    public List<FoodCard> foodCards;

    // Start is called before the first frame update
    void Start()
    {
        foreach(FoodCard foodCard in foodCards)
        {
            foodCard.setCart(cart);
            foodCard.canvasController = canvasController;
            grid.addItem(foodCard.getGameObject());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
