using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer Instance;

    [SerializeField] private AudioClip[] tracks;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.loop = false;
            audioSource.playOnAwake = false;

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
            audioSource.clip = nextTrack;
            audioSource.Play();

            // Wait until the current track finishes
            yield return new WaitForSeconds(nextTrack.length + 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
