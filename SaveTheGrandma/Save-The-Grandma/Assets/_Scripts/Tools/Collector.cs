using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Tools
{
    public override void Use()
    {
        base.Use();
        foreach (var a in ObjectToCheck)
        {
            Collectable source = a.transform.GetComponent<Collectable>();
            if (source != null)
            {
                source.Collect(ToolData.TypeOfTool);
                _lastTimeUsed = Time.time;
                break;
            }
        }
    }

  
}
