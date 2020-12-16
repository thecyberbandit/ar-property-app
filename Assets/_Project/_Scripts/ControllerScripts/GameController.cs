using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public bool trackerFound;
    int timer;
    public int timeOutValue;
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        timer = 0;
        trackerFound = false;
        StartTimer();	
    }
	public void StartTimer()
    {
        UIController.instance.CameraButtonInteractive(false);
        UIController.instance.TriggerMarkerText(true);
        timer = 0;
        InvokeRepeating("Timer_IR", 1f, 1f);
    }
	void Timer_IR()
    {
        timer++;
        //Debug.Log(timer);
        if(timer>timeOutValue)
        {
            //change the scene to main menu
            SceneController.instance.LoadScene(0);
        }
        if(trackerFound)
        {
            CancelInvoke("Timer_IR");
        }
    }
}
