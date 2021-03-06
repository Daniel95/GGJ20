﻿using UnityEngine;

[CreateAssetMenu(fileName = "HeatingTask", menuName = "ScriptableObjects/HeatingTask", order = 1)]
public class HeatingTaskScriptableObject : TaskScriptableObject
{
    [Range(0.0f, 1.0f)] 
    public float targetHeatPercentage;

    public override WorkManager.TaskType GetTaskType()
    {
        return WorkManager.TaskType.Heating;
    }
}