using UnityEngine;

public class IslandBoatPort : MonoBehaviour
{

    [SerializeField] private Transform _enemySpawnPositionOnIsland;
    [SerializeField] private Animator _anim;

    private bool _canTransfer;
    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _canTransfer = true;
    }
    public void CarryEnemyToIsland(Enemy _enemyToSpawn)
    {
        if (_canTransfer)
        {
            Instantiate(_enemyToSpawn, _enemySpawnPositionOnIsland.position, Quaternion.identity);
        }
    }
    public void CarryEnemyAnimation()
    {
        _anim.SetTrigger("enemyTransfer");
    }
    public void SetTransfer(bool state)
    {
        _canTransfer = state;
    }

}
