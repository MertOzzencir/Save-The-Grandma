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

    void Awake()
    {
        _bridge.OnComplete += BridgeTask;
        _fences.OnComplete += FenceTask;
    }

    private void FenceTask()
    {
        _fences.gameObject.layer = 0;
        Destroy(BoatPort.gameObject);
    }

    private void BridgeTask()
    {
        _bridge.gameObject.layer = 9;
        Vector3 localScaleBridge = _nextIsland.transform.localScale;
        PathColliderManager.UpdatePathMeshCollider(_nextIsland.GetComponent<Collider>());
        _nextIsland.transform.localScale = Vector3.zero;
        _nextIsland.SetActive(true);
        TweenManager.ScaleObject(_nextIsland.transform,localScaleBridge, 1f,DG.Tweening.Ease.InBounce);
        TweenManager.ScaleObject(_nextIslandCloud.transform, Vector3.zero, 1f, DG.Tweening.Ease.InBounce);
        TweenManager.MoveObject(_nextIsland.transform, _nextIslandMovePosition, 1f, DG.Tweening.Ease.InElastic);
        Destroy(_nextIslandCloud, 1.1f);
    }
}
