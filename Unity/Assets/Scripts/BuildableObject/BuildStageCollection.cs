﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStageCollection : MonoBehaviour
{
    private int remaining;
    private int nextIndex;
    public BuildStage[] stages = new BuildStage[0];
    public BuildStage currentBuildStage;

    public void Init()
    {
        // do we have any stages connected?
        if (stages.Length == 0)
            throw new ArgumentException("BuildStageCollection must have atleast one stage");

        remaining = stages.Length;
        nextIndex = 0;

        currentBuildStage = GetNext();
        currentBuildStage.Init();
    }

    public int Remaining()
    {
        return remaining;
    }

    public BuildStage GetNext()
    {
        if (nextIndex != stages.Length)
        {
            BuildStage nextBuildStage = stages[nextIndex];
            nextIndex++;
            remaining--;
            return nextBuildStage;
        }
        else
            throw new ArgumentOutOfRangeException("BuildStageCollection: GetNext on last item");
    }
}
