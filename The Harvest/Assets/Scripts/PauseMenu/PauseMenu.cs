using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // What should be stopped by Pause:

    // Movement, Timers, and Animations (Auto Stopped)

    // Ability to Click Seeds + Ice Seeds + Boss Seeds (DONE)
    // Ability to Click Asteroids (DONE)
    // Ability to Click Enemy Boss (DONE)
    // Ability to Use F for Iceplant (DONE)
    // Ability to Click Birds (DONE)
    // Ability to Use Keyboard for Bomber Birds (DONE)

    // The Spawning of Asteroids and Birds // DONE

    // So Pause Menu won't lock up stuff when you die and are paused:
    void Start() {
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // For Buttons:
    [SerializeField] Sprite originalButtonImageResume;
    [SerializeField] Sprite hoverButtonImageResume;
    [SerializeField] Sprite pressedButtonImageResume;

    [SerializeField] Sprite originalButtonImageMainMenu;
    [SerializeField] Sprite hoverButtonImageMainMenu;
    [SerializeField] Sprite pressedButtonImageMainMenu;

    [SerializeField] Sprite originalButtonImageLastCheckpoint;
    [SerializeField] Sprite hoverButtonImageLastCheckpoint;
    [SerializeField] Sprite pressedButtonImageLastCheckpoint;
    float delayTime = 0.1f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonHoverSound;
    [SerializeField] AudioClip buttonClickSound;

    bool buttonCanBeClicked = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                audioSource.clip = buttonClickSound;
                audioSource.Play();
                Resume();
            }
            else {
                audioSource.clip = buttonClickSound;
                audioSource.Play();
                Pause();
            }
        }
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        // to be able to be click || button
        GetComponentInChildren<PauseButton>().canBePressed = true;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public IEnumerator DelayResume() {
        yield return new WaitForSecondsRealtime(delayTime);
        Resume();
    }

    public IEnumerator DelayMainMenu() {
        yield return new WaitForSecondsRealtime(delayTime);
        // Remember to Unpause
        Resume();
        SceneManager.LoadScene(0);
    }

    public IEnumerator DelayLastCheckpoint() {
        yield return new WaitForSecondsRealtime(delayTime);
        // Remeber to Unpause
        Resume();
        SceneManager.LoadScene(Singleton.instance.GetCurrentIndexCheckpoint());
    }

    public IEnumerator DelayNewButtonPress() {
        buttonCanBeClicked = false;
        yield return new WaitForSecondsRealtime(delayTime);
        buttonCanBeClicked = true;
    }

    // Click Buttons:
    public void ResumeButton() {
        if(buttonCanBeClicked) {
            StartCoroutine(DelayNewButtonPress());
            audioSource.clip = buttonClickSound;
            audioSource.Play();
            GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<Image>().sprite = pressedButtonImageResume;
            StartCoroutine(DelayResume());
        }
    }

    public void MainMenuButton() {
        if(buttonCanBeClicked) {
            StartCoroutine(DelayNewButtonPress());
            audioSource.clip = buttonClickSound;
            audioSource.Play();
            GameObject.FindGameObjectWithTag("MainMenuButton").GetComponent<Image>().sprite = pressedButtonImageMainMenu;
            // Resetting:
            Singleton.instance.currentShopCurrency = 0;
            Singleton.instance.ResetCurrentLives();

            StartCoroutine(DelayMainMenu());
        }
    }

    public void LastCheckpointButton() {
        if(buttonCanBeClicked) {
            StartCoroutine(DelayNewButtonPress());
            audioSource.clip = buttonClickSound;
            audioSource.Play();
            GameObject.FindGameObjectWithTag("LastCheckpointButton").GetComponent<Image>().sprite = pressedButtonImageLastCheckpoint;
            // Resetting:
            Singleton.instance.ResetCurrentLives();

            StartCoroutine(DelayLastCheckpoint());
        }
    }

    // Hover and StopHover Buttons:
    public void HoverButtonResume() {
        GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<Image>().sprite = hoverButtonImageResume;
        audioSource.clip = buttonHoverSound;
        audioSource.Play();
    }

    public void HoverButtonMainMenu() {
        GameObject.FindGameObjectWithTag("MainMenuButton").GetComponent<Image>().sprite = hoverButtonImageMainMenu;
        audioSource.clip = buttonHoverSound;
        audioSource.Play();
    }

    public void HoverButtonLastCheckpoint() {
        GameObject.FindGameObjectWithTag("LastCheckpointButton").GetComponent<Image>().sprite = hoverButtonImageLastCheckpoint;
        audioSource.clip = buttonHoverSound;
        audioSource.Play();
    }

    public void StopHoverButtonResume() {
        GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<Image>().sprite = originalButtonImageResume;
    }

    public void StopHoverButtonMainMenu() {
        GameObject.FindGameObjectWithTag("MainMenuButton").GetComponent<Image>().sprite = originalButtonImageMainMenu;
    }

    public void StopHoverButtonLastCheckpoint() {
        GameObject.FindGameObjectWithTag("LastCheckpointButton").GetComponent<Image>().sprite = originalButtonImageLastCheckpoint;
    }
}
