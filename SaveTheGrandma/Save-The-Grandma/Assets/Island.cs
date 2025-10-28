using System;
using UnityEngine;

public class Island : MonoBehaviour
{
    [Header("Objects in Island")]
    [SerializeField] private Builder _bridge;
    [SerializeField] private Builder _fences;
    public IslandBoatPort BoatPort;
    [Header("Next Island")]
    [SerializeField] private Island _nextIsland;
    [SerializeField] private GameObject _nextIslandCloud;

    public virtual void Awake()
    {
        _bridge.OnComplete += BridgeTask;
        _fences.OnComplete += FenceTask;
        Invoke(nameof(SetCloud), 2f);
    }
    private void SetCloud()
    {
        _nextIslandCloud.SetActive(true);
    }

    private void FenceTask()
    {
        _fences.gameObject.layer = 0;
        BoatPort.SetTransfer(false);
        Destroy(BoatPort.gameObject,.15f);
    }

    private void BridgeTask()
    {
        Vector3 localScaleBridge = _nextIsland.transform.localScale;
        TweenManager.ScaleObject(_nextIslandCloud.transform, localScaleBridge/2, 1f, DG.Tweening.Ease.Linear);
        _bridge.gameObject.layer = 9;
        _nextIsland.BoatPort.gameObject.SetActive(true);
        PathColliderManager.UpdatePathMeshCollider(_nextIsland.GetComponent<Collider>());
        _nextIsland.transform.localScale = Vector3.zero;
        TweenManager.ScaleObject(_nextIsland.transform,localScaleBridge, 2f,DG.Tweening.Ease.InBack);
        Destroy(_nextIslandCloud, 1.1f);
    }
}
