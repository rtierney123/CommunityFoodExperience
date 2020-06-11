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
    public Text costText;
    public Text wicText;
    public Text premadeText;
    public Button minusButton;

    public Image foodImage;
    public Shelf shelf;
    public CanvasController canvasController;
    public NavigationManager navManager;

    [HideInInspector]
    public Food food { get; set; }
    [HideInInspector]
    public Cart cart { get; set; }
    [HideInInspector]
    public bool inCart = false;

    public FoodDetail detailPopup;
   
    public Vector3 resetPosition;
    public Transform startParent;
   
    private static int foodID { get; set; }

    private Transform canvasTransform;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.localPosition ;
        startParent = this.transform.parent;

        display();

        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        shelf = GameObject.Find("Shelf").GetComponent<Shelf>();
        navManager = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    void Awake()
    {
        foodID = foodID + 1;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (!inCart)
        {
            transform.position = Input.mousePosition;
            OnBeginDrag(eventData);
        }
     
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvasTransform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!inCart)
        {
            if (cart != null)
            {
                cart.notifyDroppedFood(transform.position, food);
                transform.SetParent(startParent);
                transform.localPosition = resetPosition;
            }
            else
            {
                Debug.Log("No cart");
            }
        }
      
        
    }

    public void openDetails()
    {
        if (canvasController != null)
        {
            //detailPopup = GameObject.Find("FoodDetailPopup").GetComponent<GameObject>();
            int index = food.imgPath.IndexOf(".");
            string fixedPath = "";
            if (index > 0)
                fixedPath = food.imgPath.Substring(1, index - 1);
            // Change folder "food Icons" -> "food cards"
            int index_c = fixedPath.IndexOf('/');
            int index_c2 = fixedPath.IndexOf('/', index_c + 1);
            string fixedPath_c = fixedPath.Substring(0, index_c + 1) + "Food Cards";
            string name = fixedPath.Substring(index_c2, fixedPath.Length - index_c2) + "_c";

            if (ResourceHandler.setImage(shelf.foodImage_c, fixedPath_c + name, 40) == null)
            {
                string appendix = "";
                if (navManager.currentLocation.locationId == LocationID.FoodTiger)
                {
                    fixedPath_c += "/ft";
                    appendix = "_ft";
                }
                else if (navManager.currentLocation.locationId == LocationID.CornerStore)
                {
                    fixedPath_c += "/cs";
                    appendix = "_cs";
                }
                else if (navManager.currentLocation.locationId == LocationID.FoodPantry)
                {
                    fixedPath_c += "/fp";
                    appendix = "_fp";
                }
                ResourceHandler.setImage(shelf.foodImage_c, fixedPath_c + name + appendix, 40);
            }
            canvasController.openPopup(shelf.detailPopup);
        }
        else
        {
            Debug.Log("CanvasController not set");
        }
    }

    public void closeDetails()
    {
        if (canvasController != null)
        {
            Debug.Log("close details");
            canvasController.closePopUp(detailPopup.gameObject);
        }
        else
        {
            Debug.Log("CanvasController not set");
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

            int index = food.imgPath.IndexOf(".");
            string fixedPath = "";
            if (index > 0)
                 fixedPath = food.imgPath.Substring(1, index-1);
            ResourceHandler.setImage(foodImage, fixedPath);

            costText.text = FormatText.formatCost(food.price);

            if(food.wic)
            {
                string wicPrefix = "WIC: ";
                string wicStr = "";
                FoodType[] types = food.wicType;
                foreach(FoodType type in types)
                {
                    if (!string.IsNullOrEmpty(wicStr))
                    {
                        wicStr += "/";
                    }
                    if(type == FoodType.Veg)
                    {
                        wicStr += "veg";
                    }
                    else
                    {
                        wicStr += type.toDescriptionString();
                    }
                   
                }
                wicStr = wicPrefix + wicStr;
                wicText.text = wicStr;
                wicText.gameObject.SetActive(true);
            }
            if (food.premade)
            {
                premadeText.gameObject.SetActive(true);
            }
        }
    }

   
}
