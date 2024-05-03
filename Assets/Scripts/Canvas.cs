using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void HintsButton()
    {
        Time.timeScale = 0;
        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void CancelButton()
    {
        Time.timeScale = 1;
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
