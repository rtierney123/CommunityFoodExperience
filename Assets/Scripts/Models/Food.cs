using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Food : MonoBehaviour, UnityEngine.EventSystems.IDragHandler, IEndDragHandler
{

    public Text nameText;
    public Text caloriesText;
    public Text fatText;
    public Text proteinText;
    public Text grainText;
    public Text fruitText;
    public Text vegText;
    public Text extraText;

    public string name;
    public double cost;
    public double calories;
    private double fat;
    private double satFat;
    private double cholestrol;
    private double sodium;
    private double carbs;
    private double fiber;
    private double sugar;
    private double protein;
    private bool wic;

    public double fruit;
    public double veg;
    public double grain;
    public double dairy;
    public double macroProtein;
    public double macroFat;
    public double extra;

    public FoodType wicType;

    public GameObject cartObject;

    [HideInInspector]
    public Cart cart;
    [HideInInspector]
    public CanvasController canvasController;

    public GameObject backCard;

    private Vector3 resetPosition;
    private Transform startParent;
    
    private static int foodID { get; set; }

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
            cart.notifyDroppedFood(transform.position, this);
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
        startParent = this.transform.parent;
        cartObject = this.gameObject;

        nameText.text = name;
        caloriesText.text = calories.ToString();
        fatText.text = fat.ToString();
        proteinText.text = protein.ToString();
        grainText.text = grain.ToString();
        fruitText.text = fruit.ToString();
        vegText.text = veg.ToString();
        extraText.text = extra.ToString();

    }

    void Awake()
    {
        foodID = foodID + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCart(Cart c)
    {
        cart = c;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

}
