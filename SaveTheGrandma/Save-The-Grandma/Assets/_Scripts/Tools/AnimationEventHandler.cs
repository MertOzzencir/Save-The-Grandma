using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action<int> OnAnimEventFire;
    public void AnimationEventInformation(int index)
    {
        OnAnimEventFire?.Invoke(index);
    }
   
}
