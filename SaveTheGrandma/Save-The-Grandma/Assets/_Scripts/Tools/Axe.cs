using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Axe : Tools
{


    public override void Use()
    {
         if (!CooldownTimer())
            return;

        base.Use();
        foreach (var a in ObjectToCheck)
        {
            MotherSource source = a.transform.GetComponent<MotherSource>();
            if (source != null)
            {
                source.Dig(ToolData.AttackDamage, ToolData.TypeOfTool);
                _lastTimeUsed = Time.time;
                break;
            }
        }
    }
    
   




}
