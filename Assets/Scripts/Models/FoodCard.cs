using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class FoodCard : MonoBehaviour, UnityEngine.EventSystems.IDragHandler, IEndDragHandler
{

    public Text nameText;
    public Text caloriesText;
    public Text fatText;
    public Text proteinText;
    public Text grainText;
    public Text fruitText;
    public Text vegText;
    public Text extraText;

    public Text costText;

    public Text wicText;
    public Text premadeText;

    public Image foodImage;
    public CanvasController canvasController;

    [HideInInspector]
    public Food food { get; set; }
    [HideInInspector]
    public Cart cart { get; set; }


    public GameObject backCard;

   
    private Vector3 resetPosition;
    private Transform startParent;
   
    private static int foodID { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
        startParent = this.transform.parent;


        display();

        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }

    void Awake()
    {
        foodID = foodID + 1;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        OnBeginDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        transform.SetParent(canvas);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(cart != null)
        {
            cart.notifyDroppedFood(transform.position, food);
            transform.SetParent(startParent);
            transform.position = resetPosition;
        } else
        {
            Debug.Log("No cart");
        }
        
    }

    public void flipCardBack()
    {
        if(canvasController != null)
        {
            Debug.Log("flip card back");
            canvasController.openPopup(backCard);
        } else
        {
            Debug.Log("CanvasController not set");
        }
    }

    public void flipCardForward()
    {
        if (canvasController != null)
        {
            canvasController.closePopUp(backCard);
        } else
        {
            Debug.Log("Cannot flip, canvas controller is null.");
        }
    }


    public void setCart(Cart c)
    {
        cart = c;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public void setFood(Food foodItem)
    {
        food = foodItem;
        display();
    }

    private void display()
    {
        if (food != null)
        {
            nameText.text = food.name;
            caloriesText.text = FormatText.formatDouble(food.calories);
            fatText.text = FormatText.formatDouble(food.nutrition.fat);
            proteinText.text = FormatText.formatDouble(food.nutrition.protein);
            grainText.text = FormatText.formatDouble(food.nutrition.grain);
            fruitText.text = FormatText.formatDouble(food.nutrition.fruit);
            vegText.text = FormatText.formatDouble(food.nutrition.veg);
            extraText.text = FormatText.formatDouble(food.nutrition.extra);

            int index = food.imgPath.IndexOf(".");
            string fixedPath = "";
            if (index > 0)
                 fixedPath = food.imgPath.Substring(1, index-1);
            ResourceHandler.setImage(foodImage, fixedPath);

            costText.text = FormatText.formatCost(food.price);

            if(food.wic)
            {
                wicText.gameObject.SetActive(true);
            }
            if (food.premade)
            {
                premadeText.gameObject.SetActive(true);
            }
        }
    }

   
}
