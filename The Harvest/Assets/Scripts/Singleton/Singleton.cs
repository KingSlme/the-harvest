using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    // Shop:
    public bool hasWarHorn = false;
    public bool hasFrostbite = false;
    public bool hasFertilizer = false;
    public bool hasAutoSeeder = false;

    public float warHornCooldown = 0f;
    public float frostbiteCooldown = 0f;
    public float fertilizerCooldown = 0f;
    public float autoSeederCooldown = 0f;

    // Adding this to live resets cus no time :V
    public void resetCooldowns() {
        warHornCooldown = 0f;
        frostbiteCooldown = 0f;
        fertilizerCooldown = 0f;
        autoSeederCooldown = 0f;
    }

    private int currentLives = 3;
    public static Singleton instance = null;

    public AudioSource audioSource;
    public AudioSource audioSourceCompletion;
    public AudioSource audioSourceHit;
    public AudioSource audioSourceBirdDeath;
    public AudioSource audioSourceBirdExplode;
    public AudioSource audioSourceShoot;

    [SerializeField] AudioClip[] audioClipsCollect;
    [SerializeField] AudioClip completion;
    [SerializeField] AudioClip hitBird;
    [SerializeField] AudioClip birdDeath;
    [SerializeField] AudioClip birdExplode;
    [SerializeField] AudioClip shootSeed;

    // Music (Second to Last Audio Source in Inspector)
    public AudioSource audioSourceMusic;
    [SerializeField] AudioClip[] audioClipsMusic;

    // For Checkpoints
    int currentIndexCheckpoint = 1;

    public int currentShopCurrency = 0;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }
        DontDestroyOnLoad (gameObject);
    }

    public int GetCurrentLives() {
        return currentLives;
    }

    public void ResetCurrentLives() {
        currentLives = 3;
        resetCooldowns();
    }

    public void ResetBossCurrentLives() {
        currentLives = 1;
        resetCooldowns();
    }

    public void IncremenetCurrentLives() {
        currentLives++;
    }

    public void DecrementCurrentLives() {
        currentLives--;
    }

    private AudioClip GetRandomAudioClipCollect() {
        return audioClipsCollect[Random.Range(0,2)];
    }

    public void PlayCollectSound() {
        audioSource.clip = GetRandomAudioClipCollect();
        audioSource.Play();
    }

    public void PlayCompletionSound() {
        audioSourceCompletion.clip = completion;
        audioSourceCompletion.Play();
    }

    public void PlayBirdHitSound() {
        audioSourceHit.clip = hitBird;
        audioSourceHit.Play();
    }

    public void PlayBirdDeathSound() {
        audioSourceBirdDeath.clip = birdDeath;
        audioSourceBirdDeath.Play();
    }

    public void PlayBirdExplodeSound() {
        audioSourceBirdExplode.clip = birdExplode;
        audioSourceBirdExplode.Play();
    }

    public void PlayShootSound() {
        audioSourceShoot.clip = shootSeed;
        audioSourceShoot.Play();
    }

    // Music
    public void FindNextMusic() {
        // Forest == 1
        // Desert = 4
        // Ice = 7
        // Space = 10
        // Boss = 13
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentIndex == 1) {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = audioClipsMusic[0];
            currentIndexCheckpoint = 1;
        }
        else if(currentIndex == 4) {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = audioClipsMusic[1];
            currentIndexCheckpoint = 4;
        }
        else if(currentIndex == 7) {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = audioClipsMusic[2];
            currentIndexCheckpoint = 7;
        }
        else if(currentIndex == 10) {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = audioClipsMusic[3];
            currentIndexCheckpoint = 10;
        }
        else if(currentIndex == 13) {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = audioClipsMusic[4];
            currentIndexCheckpoint = 13;
        }
        // Store
        else if(currentIndex == 16) {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = audioClipsMusic[5];
        }
    }

    public void PlayMusic() {
        if(!audioSourceMusic.isPlaying) {
            // Make sure music isn't looping in inspector
            audioSourceMusic.Play();
        }
    }

    public void StopMusic() {
        audioSourceMusic.Stop();
    }

    public int GetCurrentIndexCheckpoint() {
        return currentIndexCheckpoint;
    }
}
