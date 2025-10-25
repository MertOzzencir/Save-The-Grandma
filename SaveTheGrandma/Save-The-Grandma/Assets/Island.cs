using System;
using UnityEngine;

public class Island : MonoBehaviour
{
    [Header("Objects in Island")]
    [SerializeField] private Builder _bridge;
    [SerializeField] private Builder _fences;
    public IslandBoatPort BoatPort;
    [Header("Next Island")]
    [SerializeField] private GameObject _nextIsland;
    [SerializeField] private GameObject _nextIslandCloud;
    [SerializeField] private Vector3 _nextIslandMovePosition;

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
        TweenManager.ScaleObject(_nextIslandCloud.transform, Vector3.zero, 1f, DG.Tweening.Ease.Linear);
        _bridge.gameObject.layer = 9;
        _nextIsland.SetActive(true);
        Vector3 localScaleBridge = _nextIsland.transform.localScale;
        PathColliderManager.UpdatePathMeshCollider(_nextIsland.GetComponent<Collider>());
        _nextIsland.transform.localScale = Vector3.zero;
        _nextIsland.SetActive(true);
        TweenManager.ScaleObject(_nextIsland.transform,localScaleBridge, 2f,DG.Tweening.Ease.InBack);
        TweenManager.MoveObject(_nextIsland.transform, _nextIslandMovePosition, 1f, DG.Tweening.Ease.InBack);
        Destroy(_nextIslandCloud, 1.1f);
    }
}
