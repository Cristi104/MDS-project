using UnityEngine;

public class MyceneManager : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        
    }
}
