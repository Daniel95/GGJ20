﻿using UnityEngine;

[CreateAssetMenu(fileName = "CleaningTask", menuName = "ScriptableObjects/CleaningTask", order = 1)]
public class CleaningTaskScriptableObject : TaskScriptableObject
{
    public int TargetCleanliness;
    public override WorkManager.TaskType GetTaskType()
    {
        return WorkManager.TaskType.Cleaning;
    }
}