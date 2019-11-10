using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Manage;


public class ClockDisplay : MonoBehaviour
{
	public GameObject endScreen;
	public bool running;
	public uint runtimeMiliSeconds;
	private DateTime startTime;
	public uint amStartTime;
	public uint pmEndTime;
	public Text txt;
	public Animator anim;
	public TimeSpan pauseTime;
	public DateTime pauseStart;
	public GameManager gameManager;
	
    // Start is called before the first frame update
    void Start()
    {
		running = false;
		anim.speed = 0.0001f;
		startTime = DateTime.Now;
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

    // Update is called once per frame
    void Update()
    {
		if (!this.running) {
			return;
		}
		TimeSpan time = (DateTime.Now - startTime - this.pauseTime);
		float runTimeRatio = (float)time.TotalMilliseconds / runtimeMiliSeconds;
		anim.Play("ClockAnimation", 0, runTimeRatio);

		if (runTimeRatio >= 1f) {
			endScreen.active = true;
			gameManager.pause();
		}

		string result = runTimeToDayTime(runTimeRatio);
		txt.text = result;
	}

	public void pause() {
		this.running = false;
		this.pauseStart = DateTime.Now;
	}

	public void resume() {
		this.pauseTime += DateTime.Now - this.pauseStart;
		this.running = true;
	}
}
