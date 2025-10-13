using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [System.Serializable]
    public struct Sound
    {
        public SoundEffect soundType;
        public AudioClip clip;
    }

    [Header("Registered Sounds")]
    [SerializeField] private Sound[] sounds;

    private Dictionary<SoundEffect, AudioClip> soundDict;

    // TEST STATMENT
    private void Start()
    {
        Debug.Log("AudioManager test: playing Gunshot1...");
        PlaySFX(SoundEffect.Gunshot1);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildSoundDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void BuildSoundDictionary()
    {
        soundDict = new Dictionary<SoundEffect, AudioClip>();
        foreach (var s in sounds)
        {
            if (!soundDict.ContainsKey(s.soundType))
            {
                soundDict.Add(s.soundType, s.clip);
            }
        }
    }

    public void PlaySFX(SoundEffect sound, Vector3 position, float volume = 1f)
    {
        if (soundDict.TryGetValue(sound, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
        else
        {
            Debug.LogWarning($"AudioManager: No clip found for {sound}");
        }
    }

    public void PlaySFX(SoundEffect sound, float volume = 1f)
    {
        if (soundDict.TryGetValue(sound, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"AudioManager: No clip found for {sound}");
        }
    }
}
