using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityIndicatorHandler : MonoBehaviour
{
    [SerializeField] private GameObject _eatIndicator;
    [SerializeField] private SpriteRenderer _stateIconGameObject;


    private Transform _lastMaterial;
    private GameObject _lastIndicator;
    public void ImplementEatIndicator(Transform _eatObject)
    {
        if (_lastMaterial != _eatObject)
        {
            _lastIndicator= Instantiate(_eatIndicator, _eatObject.position + new Vector3(0, 2f, 0), Quaternion.identity);
            _lastIndicator.transform.parent = _eatObject.transform;
            _lastMaterial = _eatObject;
        }
    }
    public void RestartIndicator()
    {
        if (_lastMaterial != null)
        {
            Destroy(_lastIndicator.gameObject);
            _lastMaterial = null;
        }
    }
    public void StateIconHandle(Sprite sprite)
    {
        _stateIconGameObject.sprite = sprite;
    }

}
