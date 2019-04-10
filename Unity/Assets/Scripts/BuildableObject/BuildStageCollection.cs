using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStageCollection : MonoBehaviour
{
    private int currIndex = 0;
    public BuildStage[] stages = new BuildStage[0];

    public void Init()
    {
        stages[0].gameObject.SetActive(true);
        int i = 1;
        while (i != stages.Length)
        {
            stages[i].gameObject.SetActive(false);
            i++;
        }
    }

    public int Count()
    {
        return stages.Length - currIndex;
    }

    public BuildStage Dequeue()
    {
        if (currIndex >= stages.Length)
            throw new IndexOutOfRangeException();
        int tmp = currIndex;
        currIndex++;
        return stages[tmp];
    }
}
