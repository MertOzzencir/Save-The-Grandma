using System;
using UnityEngine;

public class RockIsland : Island
{
    [SerializeField] private Builder RockPath;

    public override void Awake()
    {
        base.Awake();
        RockPath.OnComplete += RockTask;
    }

    private void RockTask()
    {
        RockPath.gameObject.layer = 0;
        Debug.Log("Rock Finished");
    }
}
