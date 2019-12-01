
using Manage;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    public NavigationManager navigationManager;
    public GameObject farePopUp;
    public GameObject stopPopUp;
    public MapLocations mapLocation;

    public MessageManager messageManager;

    public Material material;

    public int randStuckPercent = 50;
    public float stuckSeconds = 30;

    [HideInInspector]
    public bool atStop;
    [HideInInspector]
    public bool playerOnBus;

    private Animator animator;

    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        pauseAnimation();
    }

 
    // Update is called once per frame
    void Update()
    {
        material.color = navigationManager.OnBus ? new Color(255, 0, 0) : (material.color.r == 255 && material.color.g == 0 && material.color.b == 0 ? new Color(0, 0, 0) : material.color);
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
            navigationManager.handleBusStoppedEvent();
        }
    }

    public void resumeRoute()
    {
        mapLocation = MapLocations.OnRoad;
        atStop = false;
        navigationManager.handleBusLeavingEvent();
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
        resetAnimation();
        resumeAnimation();
    }

    public void resetAnimation()
    {
        if(animator != null)
        {
            animator.Play("", 0, 0f);
        }
        
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