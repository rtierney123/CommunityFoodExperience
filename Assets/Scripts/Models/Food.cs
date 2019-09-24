using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Food : MonoBehaviour, UnityEngine.EventSystems.IDragHandler, IEndDragHandler
{
    public int fruit;
    public int veg;
    public int grain;
    public int fat;
    public int dairy;
    public int protein;
    public int extra;
    public int calories;

    public int cost;

    public GameObject cartObject;

    private Vector3 resetPosition;

    [HideInInspector]
    public bool isDragged;
    public Cart cart;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cart.notifyDroppedFood(transform.position, this);
        transform.position = resetPosition;
    }


    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCart(Cart c)
    {
        cart = c;
    }
}
