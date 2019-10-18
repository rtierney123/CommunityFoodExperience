using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
<<<<<<< Updated upstream
    public Food[] content;
=======
    public CanvasController canvasController;
    public FoodCard[] content;
>>>>>>> Stashed changes
    public Cart cart;
    public PopulateGrid grid;

    // Start is called before the first frame update
    void Start()
    {
        foreach(FoodCard food in content)
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
