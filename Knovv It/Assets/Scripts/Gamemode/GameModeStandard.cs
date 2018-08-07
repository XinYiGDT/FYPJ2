using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeStandard : GameModeBase
{
    Text gameModeTimerObj;
    const float gameModeTimer = 180f;
    const float turnTimer = 15f;

    public override void SetUp()
    {
        if (GameObject.Find("GameTimer").GetComponent<Text>())
        {
            gameModeTimerObj = GameObject.Find("GameTimer").GetComponent<Text>();
            gameModeTimerObj.text = gameModeTimer.ToString();
        }
    }

    public override void TearDown()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
