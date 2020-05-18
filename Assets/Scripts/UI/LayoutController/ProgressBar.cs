using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float loadTime;
    public int displayIntervals;
    public Slider slider;

    private float currentTime = 0;
    private bool loading = false;
    private bool pause = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (this.gameObject.activeInHierarchy && !loading)
        {
            Debug.Log("Start Load");
            loading = true;
            StartCoroutine(Load());
            
        }
        else if(!this.gameObject.activeInHierarchy)
        {
            resetLoading();
            Debug.Log("Not Active");
        }

    }

    public IEnumerator Load()
    {
        while (loading)
        {
            if (!pause)
            {
                float incrementTime = loadTime / displayIntervals;
                yield return new WaitForSeconds(incrementTime);
                currentTime += incrementTime;
                slider.value = (float)(currentTime / loadTime);
            }

            if(currentTime < loadTime)
            {
                loading = false;
            }
        }

    }

    public bool getComplete()
    {
        return currentTime >= loadTime;
    }

    public void resetLoading()
    {
        currentTime = 0;
        slider.value = 0;
        loading = false;
    }

    public void pauseLoading()
    {
        pause = true;
    }

    public void resumeLoading()
    {
        pause = false;
    }

}
