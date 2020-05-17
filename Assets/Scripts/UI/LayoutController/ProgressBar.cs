using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float loadTime;
    public int displayIntervals;
    public Slider slider;

    private float currentTime = 0;
    private bool loading = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (this.gameObject.activeInHierarchy && !loading)
        {
            StartCoroutine(Load());
            Debug.Log("Start Load");
            loading = true;
        }
        else if(!this.gameObject.activeInHierarchy)
        {
            currentTime = 0;
            slider.value = 0;
            loading = false;
            Debug.Log("Not Active");
        }

    }

    public IEnumerator Load()
    {
        while(currentTime < loadTime)
        {
            float incrementTime = loadTime / displayIntervals;
            yield return new WaitForSeconds(incrementTime);
            currentTime += incrementTime;
            slider.value = (float)(currentTime/ loadTime);
        }

    }

    public bool getComplete()
    {
        return currentTime >= loadTime;
    }

}
