using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage;

public class BusController : MonoBehaviour
{
	public NavigationManager navigation;
    public MessageManager messageManager;
	public Animator animator;
	public Material material;

    public int randPercent = 50;
    public float stuckSeconds = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void Pause()
	{
		animator.enabled = false;
	}

	public void Play()
	{
		animator.enabled = true;
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

    // Update is called once per frame
    void Update()
    {
		
		material.color = navigation.OnBus ? new Color(255, 0, 0) : (material.color.r == 255 && material.color.g == 0 && material.color.b == 0 ? new Color(0,0,0) : material.color);
    }

    public void stuckEvent()
    {
        float rand = Random.Range(0, 100);
        if(rand < randPercent)
        {
            StartCoroutine(startStuck());
        }
     
    }

    private IEnumerator startStuck()
    {
        string msg = chooseRandomDelayMessage();
        messageManager.generateStandardErrorMessage(msg);
        Pause();
        yield return new WaitForSeconds(stuckSeconds);
        Play();
    }

    private string chooseRandomDelayMessage()
    {
        float rand = Random.Range(0, 4);
        if(rand < 0)
        {
            return "Bus got a flat tire. Bus route delayed.";
        } else if(rand < 1)
        {
            return "Bus driver got hungry. Bus route delayed.";
        } else if (rand < 2)
        {
            return "Bad traffic. Bus route delayed.";
        } else
        {
            return "Parade. Bus route delayed.";
        }
    }
}
