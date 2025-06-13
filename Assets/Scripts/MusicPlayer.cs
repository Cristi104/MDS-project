using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] 
    private AudioClip[] tracks;

    private AudioSource _audioSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                _audioSource = gameObject.AddComponent<AudioSource>();

            _audioSource.loop = false;
            _audioSource.playOnAwake = false;

            StartCoroutine(PlayRandomLoop());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PlayRandomLoop()
    {
        while (true)
        {
            if (tracks.Length == 0)
                yield break;

            AudioClip nextTrack = tracks[Random.Range(0, tracks.Length)];
            _audioSource.clip = nextTrack;
            _audioSource.Play();

            // Wait until the current track finishes
            yield return new WaitForSeconds(nextTrack.length + 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
