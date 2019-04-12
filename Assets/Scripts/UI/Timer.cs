using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    private float timer = 900;
    private bool canCount = true;
    private bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(timer);
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;

            // Convert string
            int min = Mathf.FloorToInt(timer / 60);
            int sec = Mathf.FloorToInt(timer % 60);

            uiText.GetComponent<UnityEngine.UI.Text>().text = min.ToString("00") + ":" + sec.ToString("00");
            //uiText.text = timer.ToString("F");
        }
        else if (timer <= 0.0f && doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0";
            timer = 0.0f;
        }
    }
}
