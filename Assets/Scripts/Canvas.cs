using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    public AudioSource pauseAudio;
    public AudioSource resumeAudio;
    public AudioSource hintsAudio;
    public AudioSource cancelAudio;

    public void PauseButton()
    {
        Time.timeScale = 0;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        pauseAudio.Play();
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        resumeAudio.Play();
    }

    public void HintsButton()
    {
        Time.timeScale = 0;
        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        hintsAudio.Play();
    }

    public void CancelButton()
    {
        Time.timeScale = 1;
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        cancelAudio.Play();
    }

    public void RestartButton()
    {
        Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }



}
