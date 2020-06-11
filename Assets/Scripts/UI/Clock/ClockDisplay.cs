using System.Collections;
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
	public TimeSpan pauseTime;
    public TimeSpan lossTime;
	public DateTime pauseStart;
	public GameManager gameManager;
    public Slider runtimeSlider;

    private uint currentHour = 0;
    private uint currentMin = 0;
    private bool hourBeforeEndCalled = false;

    void Start()
    {

        eventCallers = new List<IClockEventCaller>();
        foreach (GameObject gameObject in clockCallerGameObjects)
        {
            try
            {
                IClockEventCaller caller = gameObject.GetComponent<IClockEventCaller>();
                eventCallers.Add(caller);
            } catch(Exception ex)
            {
                //Debug.Log(ex.ToString());
            }
        }
	}


    public uint getCurrentMilitaryHour()
    {
        return currentHour;
    }
    
    public uint getCurrentMinutes()
    {
        return currentMin;
    }

    public void resetAnimation()
    {
        running = false;
        currentHour = amStartTime;
        currentMin = 0;
        lossTime = TimeSpan.Zero;
        pauseTime = TimeSpan.Zero;
    }

    public void startAnimation() {
        running = true;
        hourBeforeEndCalled = false;
        startTime = DateTime.Now;
        pauseTime = TimeSpan.Zero;
        Debug.Log("start clock");
    }

    // Update is called once per frame
    void Update()
    {
		if (!this.running) {
			return;
		}
		
        float runTimeRatio = getRuntimeRatio();
        runtimeSlider.value = runTimeRatio;

        if (endGameCondition()) {
            gameManager.endGame();
		} else
        {
            string result = runTimeToDayTime(runTimeRatio);
            txt.text = result;

            uint hour = getHourFromRatio(runTimeRatio);
            uint min = getMinFromRatio(runTimeRatio);

            if (hour != amStartTime)
            {
                if (currentHour != hour)
                {
                    currentHour = hour;
                    updateHourCallers();
                    if(currentHour == pmEndTime + 11)
                    {
                        callHourBeforeEndGameEvent();
                    } 
                }
            }

            if (currentMin != min)
            {
                currentMin = min;
                updateMinCallers();
            }

        }
       
	}

    public bool endGameCondition()
    {
        float runTimeRatio = getRuntimeRatio();
        return runTimeRatio >= 1f;
    }

    string runTimeToDayTime(float ratio)
    {
        uint Hour = getHourFromRatio(ratio);
        uint twelveHour = convertHourToTwelveHour(Hour);
        uint Minutes = getMinFromRatio(ratio);
        string h = twelveHour.ToString();
        if (h.Length == 1) h = "0" + h;
        string m = Minutes.ToString();
        if (m.Length == 1) m = "0" + m;
        string a = (Hour > 11 ? "pm" : "am");

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

    private void callHourBeforeEndGameEvent()
    {
        if (!hourBeforeEndCalled)
        {
            foreach (IClockEventCaller caller in eventCallers)
             {
                caller.hourBeforeEndGame();
            }
            hourBeforeEndCalled = true;

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

    public void addGameHours(double hour)
    {
        uint numberHours = 12 - amStartTime + pmEndTime;
        double gameHourstoMilliseconds = (runtimeMiliSeconds / numberHours)*hour;
        TimeSpan lossSpan = TimeSpan.FromMilliseconds(gameHourstoMilliseconds);
        lossTime = lossTime.Add(lossSpan);

        if (endGameCondition())
        {
            gameManager.endGame();
        }
    }

    public void addGameMinutes(double min)
    {
        uint numberMins = getTotalInGameMinutes();
        double gameMinutestoMilliseconds = (runtimeMiliSeconds / numberMins)*min;
        TimeSpan lossSpan = TimeSpan.FromMilliseconds(gameMinutestoMilliseconds);
        lossTime = lossTime.Add(lossSpan);

        if (endGameCondition())
        {
            gameManager.endGame();
        }

    }

    public float convertGameMinutestoSeconds(uint min)
    {
        uint numberMins = getTotalInGameMinutes();
        float milliseconds = (runtimeMiliSeconds / numberMins) * min;
        return (float)(milliseconds * .001);
    }

    private uint getTotalInGameMinutes()
    {
        uint inGameMinutes = (12 - amStartTime + pmEndTime) * 60;
        return inGameMinutes;
    }

    private uint convertHourToTwelveHour(uint hour)
    {
        return (uint) (hour > 12 ? (hour - 12) : hour);
    }

    private uint getHourFromRatio(float ratio)
    {
       return (uint)(Math.Floor((pmEndTime + 12 - amStartTime) * ratio) + amStartTime);
    }

    private uint getMinFromRatio(float ratio)
    {
        return (uint)(Math.Floor(((pmEndTime + 12f - amStartTime) * ratio) * 60f) % 60f);
    }

    private float getRuntimeRatio()
    {
        TimeSpan time = (DateTime.Now - startTime - pauseTime + lossTime);
        return (float)time.TotalMilliseconds / runtimeMiliSeconds;
    }




}
