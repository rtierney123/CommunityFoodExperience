using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    public GameManager manager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startStop()
    {
       
        manager.handleBusStopEvent();
        StartCoroutine(PauseThenResume(1));
    }

    private IEnumerator PauseThenResume(float waitTime)
    {
        animator.enabled = false;
        yield return new WaitForSeconds(waitTime);
        animator.enabled = true;
    }
}
