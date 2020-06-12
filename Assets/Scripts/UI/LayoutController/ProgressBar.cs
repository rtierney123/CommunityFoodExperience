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



    void Update()
    {
        if (this.gameObject.activeInHierarchy && !loading)
        {
            loading = true;
            StartCoroutine(Load());
            
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
                slider.value = getDecimalDone();
            }

            if(currentTime < loadTime)
            {
                loading = false;
            }
        }

    }

    public float getDecimalDone()
    {
        return (float)(currentTime / loadTime);
    }

    public bool getComplete()
    {
        return currentTime >= loadTime;
    }

    public bool isPaused()
    {
        return pause;
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

    public void delayLoading(int delay)
    {
        if(currentTime - delay < 0)
        {
            currentTime = 0;
        }
        else
        {
            currentTime -= delay;
        }
    }

}
