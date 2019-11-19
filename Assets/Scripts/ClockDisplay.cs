﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Manage;


public class ClockDisplay : MonoBehaviour
{
    public List<GameObject> clockCallerGameObjects;
    private List<IClockEventCaller> eventCallers;

	public bool running;
	public uint runtimeMiliSeconds;
	private DateTime startTime;
	public uint amStartTime;
	public uint pmEndTime;
	public Text txt;
	public Animator anim;
	public TimeSpan pauseTime;
    public TimeSpan lossTime;
	public DateTime pauseStart;
	public GameManager gameManager;

    private int currentHour = 0;
    private int currentMin = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		running = false;
		anim.speed = 0.0001f;
		startTime = DateTime.Now;

        eventCallers = new List<IClockEventCaller>();
        foreach (GameObject gameObject in clockCallerGameObjects)
        {
            try
            {
                IClockEventCaller caller = gameObject.GetComponent<IClockEventCaller>();
                eventCallers.Add(caller);
            } catch(Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
	}


    // Update is called once per frame
    void Update()
    {
		if (!this.running) {
			return;
		}
		TimeSpan time = (DateTime.Now - startTime - this.pauseTime - lossTime);
		float runTimeRatio = (float)time.TotalMilliseconds / runtimeMiliSeconds;
		anim.Play("ClockAnimation", 0, runTimeRatio);

		if (runTimeRatio >= 1f) {
            gameManager.endGame();
		}

        int hour = (int)(Math.Floor((pmEndTime + 12 - amStartTime) * runTimeRatio) + amStartTime);
        if(currentHour != hour)
        {
            updateHourCallers();
            currentHour = hour;
        }
        int min = (int)(Math.Floor(((pmEndTime + 12f - amStartTime) * runTimeRatio) * 60f) % 60f);
        if(currentMin != min)
        {
            updateMinCallers();
            currentMin = min;
        }
        string result = runTimeToDayTime(runTimeRatio);
		txt.text = result;
	}

    string runTimeToDayTime(float run)
    {
        int Hour = (int)(Math.Floor((pmEndTime + 12 - amStartTime) * run) + amStartTime);
        int Minutes = (int)(Math.Floor(((pmEndTime + 12f - amStartTime) * run) * 60f) % 60f);
        string h = (Hour > 12 ? (Hour - 12).ToString() : Hour.ToString());
        if (h.Length == 1) h = "0" + h;
        string m = Minutes.ToString();
        if (m.Length == 1) m = "0" + m;
        string a = (Hour > 12 ? "pm" : "am");
        return h + ":" + m + a;
    }

    private void updateHourCallers()
    {
        foreach(IClockEventCaller caller in eventCallers)
        {
            caller.hourPassed();
        }
    }

    private void updateMinCallers()
    {
        foreach (IClockEventCaller caller in eventCallers)
        {
            caller.minutePassed();
        }
    }

    public void pause() {
		this.running = false;
		this.pauseStart = DateTime.Now;
	}

	public void resume() {
		this.pauseTime += DateTime.Now - this.pauseStart;
		this.running = true;
	}

    public void addRunningTime(double min)
    {
        DateTime lossTime = DateTime.Now.AddMinutes(min);
        this.lossTime += DateTime.Now - lossTime;
    }

    public double realToGameMin(double d)
    {
        double inGameHours = 12 + pmEndTime - amStartTime;
        return 60 * 60 * inGameHours / runtimeMiliSeconds * 1000 * d;
    }
}