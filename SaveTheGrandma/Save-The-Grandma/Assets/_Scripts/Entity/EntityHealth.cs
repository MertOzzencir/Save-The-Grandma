using System.Collections;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Material _damagedMaterial;
    [SerializeField] private GameObject _endScreen;

    private Material _originalMaterial;
    private MeshRenderer _visualRenderer;
    private Coroutine _damageIndicator;
    void Start()
    {
        _visualRenderer =  GetComponentInChildren<MeshRenderer>();
        _originalMaterial = _visualRenderer.material;
    }
    public void GetDamage(int damage)
    {
        _health -= damage;
        if(_damageIndicator == null)
            _damageIndicator = StartCoroutine(nameof(DamageEffect));
        if (_health <= 0)
            HandleDeath();
    }

    public void HandleDeath()
    {
        //_endScreen.SetActive(true);
        Destroy(gameObject);
        //Application.Quit(8);
    }
    IEnumerator DamageEffect()
    {
        _visualRenderer.material = _damagedMaterial;
        yield return new WaitForSeconds(0.2f);
        _visualRenderer.material = _originalMaterial;
        _damageIndicator = null;
    }
}
