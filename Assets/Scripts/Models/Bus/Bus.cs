
using Manage;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    public NavigationManager navigationManager;
    public MapLocations mapLocation;

    public MessageManager messageManager;

    public Material material;

    public int randStuckPercent = 50;
    public float stuckSeconds = 30;

    public Transform offScreenLocation;

    [HideInInspector]
    public bool atStop = false;
    [HideInInspector]
    public bool playerOnBus = false;
    [HideInInspector]
    public bool stopSelected = false;
    
    private Animator animator;

    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        pauseAnimation();
        animator.enabled = false;
    }

 
    // Update is called once per frame
    void Update()
    {
        material.color = playerOnBus ? new Color(255, 0, 0) : (material.color.r == 255 && material.color.g == 0 && material.color.b == 0 ? new Color(0, 0, 0) : material.color);
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
            {
                if (!playerOnBus)
                {
                    navigationManager.handleBusClickedEvent();
                }

            }
        }
    }

    public void startStop(MapLocations stopLocation)
    {
        mapLocation = stopLocation;
        atStop = true;
        if (playerOnBus)
        {
            navigationManager.handleBusStoppedEvent(mapLocation);
        }
    }

    public void resumeRoute()
    {
        mapLocation = MapLocations.OnRoad;
        atStop = false;
        navigationManager.handleBusContinuingEvent();
    }

    public void pauseAnimation()
    {
        if(animator != null)
        {
            animator.enabled = false;
        }
        
    }
    
    public void resumeAnimation()
    {
        if(animator != null)
        {
            animator.enabled = true;
        }
        
    }

    public void startAnimation()
    {
        setOffScreen();
        resumeAnimation();
    }

    public void setOffScreen()
    {
        this.transform.position = offScreenLocation.position;
    }

    public void playAnimation(float offset)
    {
        if (!animator.isInitialized)
        {
            animator.Rebind();
        }

        if (animator != null)
        {
            animator.PlayInFixedTime("Bus Route", 0, offset);
            //animator.Play("Bus Route", 0, (float).5);
            animator.enabled = true;
        }
    }

    public void onOffScreen()
    {
        if (!playerOnBus)
        {
            animator.enabled = false;
        }
    }

    public void setLocation(Transform transform)
    {
        this.gameObject.transform.localPosition = transform.position;
        this.gameObject.transform.rotation = transform.rotation;

    }

    void OnMouseOver()
    {
        //if bus can be highlighed condition here
        material.color = new Color(255, 255, 0);
    }

    void OnMouseExit()
    {
        material.color = Color.black;
    }

    public void stuckEvent()
    {
        float rand = Random.Range(0, 100);
        if (rand < randStuckPercent)
        {
            StartCoroutine(startStuck());
        }

    }

    private IEnumerator startStuck()
    {
        string msg = chooseRandomDelayMessage();
        messageManager.generateMainScreenOnlyErrorMessage(msg);
        pauseAnimation();
        yield return new WaitForSeconds(stuckSeconds);
        resumeAnimation();
    }

    private string chooseRandomDelayMessage()
    {
        float rand = Random.Range(0, 4);
        Debug.Log("delay bus");
        if (rand < 0)
        {
            return "Bus got a flat tire. Bus route delayed.";
        }
        else if (rand < 1)
        {
            return "Bus driver got hungry. Bus route delayed.";
        }
        else if (rand < 2)
        {
            return "Bad traffic. Bus route delayed.";
        }
        else
        {
            return "Parade. Bus route delayed.";
        }
    }
}