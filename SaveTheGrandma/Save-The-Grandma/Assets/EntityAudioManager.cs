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

    public void PlayEntityClip(int from,int to)
    {
        int _audipRandomClipIndex = Random.Range(from, to);
        
        _aS.PlayOneShot(_entityClips[_audipRandomClipIndex]);
    }

    public void PlayClipByIndex(int index)
    {
        _aS.PlayOneShot(_entityClips[index]);
    }
    public void PlayOneShotByIndex(int index)
    {
        _aS.PlayOneShot(_entityClips[index]);
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
