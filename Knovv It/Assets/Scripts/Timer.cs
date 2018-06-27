using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Image TimerBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject timesUp;
    //public Transform pos;

    public float timeStartDelay;
    public float timeUpDelay;

	// Use this for initialization
	void Start () {
        timesUp.SetActive(false);
        //timesUp.Pause();
        TimerBar = GetComponent<Image>();
        timeLeft = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeLeft > 0)
        {
            if (timeStartDelay > 0)
            {
                timeStartDelay -= Time.deltaTime;
            }
            else
            {
                timeStartDelay = 0;
                timeLeft -= Time.deltaTime;
                TimerBar.fillAmount = timeLeft / maxTime;
            }
        }
        else
        {
            if (timeUpDelay > 0)
            {
                timeUpDelay -= Time.deltaTime;
            }
            else
            { 
                timesUp.SetActive(true);
                Time.timeScale = 0;
            }
            // ParticleSystem TimesUpInstance = Instantiate(timesUp,pos);
            //timesUp.Play();
            //Destroy(TimesUpInstance, timeSpawn);
            
        }
	}

    public void ResetTimer()
    {
        timeLeft = maxTime;
        TimerBar.fillAmount = timeLeft;
    }
}
