using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource _source;
        
    // Start is called before the first frame update
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        _source.PlayOneShot(clip, 0.22f);
    }
}
