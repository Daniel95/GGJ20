﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static Action TimeExpiredEvent;
    private Image clockImage;
    private float timeLimit = 1;
    private float timeLeft = 1;
    private float fl = 0.1f;
    private float quarterMark = 0f;
    private Vector3 clockScale;
    private bool working;

    private void Start()
    {
        working = false;
        Player.StartJobEvent += OnStartJob;
        Customer.ResultTextMadeEvent += OnJobFinish;
        clockImage = GetComponent<Image>();
        clockScale = transform.localScale;
        clockImage.fillAmount = 1;
    }

    private void Update()
    {
        if (!working)
            return; 

        if(timeLeft >= 0)
        {
            //Debug.Log("clock is ticking");
            clockImage.fillAmount = 1.0f / (timeLimit / timeLeft);
            timeLeft -= Time.deltaTime;
        }
        else
        {
            //timer expired, force player back to counter
            TimeExpiredEvent?.Invoke();
            return;
        }

        if (timeLeft % quarterMark >= -0.1f && timeLeft % quarterMark < 0.1f)
        {
            //Debug.Log("FEEDBACK YES");
            clockImage.transform.localScale *= 1.05f;
            fl = 0.1f;
        }
        else
        {
            //Debug.Log("Scale timer back down");
            clockImage.transform.localScale = Vector3.Lerp(clockImage.transform.localScale, clockScale, fl);
            fl += Time.deltaTime;
        }
    }

    private void OnStartJob(WorkManager.Job job)
    {
        timeLimit = job.Time;
        timeLeft = timeLimit;
        quarterMark = timeLimit * 0.25f;    //get 25% of time
        //Debug.Log("Do it in " + job.Time + " seconds");
        working = true;
    }

    private void OnJobFinish(string s)
    {
        working = false;

        timeLeft = 0;
    }

    //private void OnEnable()
    //{
    //    Player.StartJobEvent += OnStartJob;
    //}

    //private void OnDisable()
    //{
    //    //text.text = "";
    //    Player.StartJobEvent -= OnStartJob;
    //}
}
