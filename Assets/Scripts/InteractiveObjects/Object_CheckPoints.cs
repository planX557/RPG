using UnityEngine;

public class Object_CheckPoints : MonoBehaviour, ISaveable
{
    [SerializeField] private string checkPointId;
    [SerializeField] private Transform respawnPoint;

    public bool isActive;
    private Animator anim;
    private AudioSource fireAudioSource;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        fireAudioSource = GetComponent<AudioSource>();
    }

    public string GetCheckPointId() => checkPointId;

    public Vector3 GetPosition() => respawnPoint == null ? transform.position : respawnPoint.position;

    public void ActivateCheckPoint(bool activate)
    {
        isActive = activate;
        anim.SetBool("isActive", activate);

        if (isActive && fireAudioSource.isPlaying == false)
            fireAudioSource.Play();
        
        if(isActive == false)
            fireAudioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateCheckPoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.unlockedCheckPoints.TryGetValue(checkPointId, out active);
        ActivateCheckPoint(active);
    }

    public void SaveData(ref GameData data)
    {
        if (isActive == false)
            return;

        if (data.unlockedCheckPoints.ContainsKey(checkPointId) == false)
            data.unlockedCheckPoints.Add(checkPointId, true);
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(checkPointId))
        {
            checkPointId = System.Guid.NewGuid().ToString();
        }
#endif
    }
}
