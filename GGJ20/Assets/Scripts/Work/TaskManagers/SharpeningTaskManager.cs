﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
using DG.Tweening;

public class SharpeningTaskManager : TaskManagerBase
{
    private SharpeningTaskScriptableObject curTask = null;
    private PerlinNoiseDeformer deformer = null;
    [SerializeField]
    private float deformSharpingAmount = 0.25f;

    private Transform startTransform = null;
    [SerializeField]
    private Vector3 localOffset = new Vector3(0, 0.5f, 0);

    protected override void Awake()
    {
        base.Awake();

        SwordTeleportPoint[] swordTeleportPoints = GameObject.FindObjectsOfType<SwordTeleportPoint>();
        foreach (SwordTeleportPoint p in swordTeleportPoints)
        {
            if(p.taskType == this.GetTaskType())
            {
                startTransform = p.transform;
                break;
            }
        }
    }

    public override float GetOffsetPercentage()
    {
        //float maxOffset = targetHeat;

        //if (maxHeat - targetHeat > maxOffset)
        //{
        //    maxOffset = maxHeat - targetHeat;
        //}

        //float offset = currentHeat - targetHeat;
        //float offsetPercentage = Mathf.Min(offset / maxOffset, 1);

        //return offsetPercentage;

        return 0;
    }

    public override WorkManager.TaskType GetTaskType()
    {
        return WorkManager.TaskType.Sharpening;
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void SetTaskObject(TaskScriptableObject a_taskScriptableObject)
    {
        curTask = Instantiate(a_taskScriptableObject) as SharpeningTaskScriptableObject;

        deformer = sword.GetComponentInChildren<PerlinNoiseDeformer>();
    }

    private void Update()
    {
        if (!isActivated)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            //Go down.
            sword.transform.DOLocalMove(startTransform.position - localOffset, 0.25f);
        }

        if(Input.GetMouseButtonUp(0))
        {
            //Go up.
            sword.transform.DOLocalMove(startTransform.position, 0.25f);
        }

        if (Input.GetMouseButton(0))
        {
            //Deform logic.
            deformer.MagnitudeScalar -= deformSharpingAmount * Time.deltaTime;
            deformer.MagnitudeScalar = Mathf.Max(deformer.MagnitudeScalar, 0.0f);

            if(deformer.MagnitudeScalar <= 0.0f && swordDetails.blade.localScale.z >= 0.0f)
            {
                swordDetails.blade.localScale -= new UnityEngine.Vector3(0, 0, deformSharpingAmount) * Time.deltaTime;
            }
        }
    }
}
