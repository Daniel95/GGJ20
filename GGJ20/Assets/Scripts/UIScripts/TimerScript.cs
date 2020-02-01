﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private Text text;
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Player.StartJobEvent += OnStartJob;
    }

    private void OnStartJob(string desc, int time)
    {
    }

    private void OnDisable()
    {
        //text.text = "";
        Player.StartJobEvent -= OnStartJob;
    }
}
