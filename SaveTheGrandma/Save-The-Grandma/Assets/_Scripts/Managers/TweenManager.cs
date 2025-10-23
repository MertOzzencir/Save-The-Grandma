using UnityEngine;
using DG.Tweening;
public class TweenManager : MonoBehaviour
{

    public static void ScaleObject(Transform obj, Vector3 scaleAmount, float duration, Ease animationType)
    {
        obj.DOScale(scaleAmount, duration).SetEase(Ease.Flash);
    }
    public static void MoveObject(Transform obj,Vector3 movePosition,float duration,Ease animationType)
    {
        obj.DOMove(movePosition, duration).SetEase(animationType);
    }

}
