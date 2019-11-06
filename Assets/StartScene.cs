using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
	public GameObject startScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void StartGame()
	{
		startScreen.active = false;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
