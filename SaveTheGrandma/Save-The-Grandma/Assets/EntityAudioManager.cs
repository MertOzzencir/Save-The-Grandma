using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EntityAudioManager : MonoBehaviour
{
    private AudioSource _aS;
    [SerializeField] private AudioClip[] _entityClips;

    void Awake()
    {
        _aS = GetComponent<AudioSource>();
    }

    public void PlayEntityClip()
    {
        _aS.Stop();
        int _audipRandomClipIndex = Random.Range(0, _entityClips.Length);
        _aS.clip = _entityClips[_audipRandomClipIndex];
        _aS.Play();
    }

    public void PlayClipByIndex(int index)
    {
        _aS.Stop();
        _aS.clip = _entityClips[index];
        _aS.Play();
    }

    public void VolumeSet(float vol)
    {
        _aS.volume = vol;
    }
    public void SetRandomPitch()
    {
        float randomPitch = Random.Range(.5f, 1.2f);
        _aS.pitch = randomPitch;
    }
}
