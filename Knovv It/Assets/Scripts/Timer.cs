using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Image TimerBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject timesUp;

	// Use this for initialization
	void Start () {
        timesUp.gameObject.SetActive(false);
        //timesUp.Pause();
        TimerBar = GetComponent<Image>();
        timeLeft = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            TimerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            timesUp.SetActive(true);
            //ParticleSystem.EmissionModule module = timesUp.emission;
            //module.enabled = true;
            //timesUp.Play();
            Time.timeScale = 0;
        }
	}
}
