using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Sound Effects")]
    public AudioClip clickSFX;
    public AudioClip levelUpSFX;
    public AudioClip swordSlashSFX;
    public AudioClip playerHitSFX;
    public AudioClip playerUpgradeSFX;
    public AudioClip expPickupSFX;

    [Header("Music")]
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // Convenience methods
    public void PlayClick() => PlaySFX(clickSFX);
    public void PlayLevelUp() => PlaySFX(levelUpSFX);
    public void PlaySwordSlash() => PlaySFX(swordSlashSFX);
    public void PlayPlayerHit() => PlaySFX(playerHitSFX);
    public void PlayPlayerUpgrade() => PlaySFX(playerUpgradeSFX);
    public void PlayEXPPickup() => PlaySFX(expPickupSFX);
}
