using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{

	public Animator animator;
	public Material material;
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
        
    }
}
