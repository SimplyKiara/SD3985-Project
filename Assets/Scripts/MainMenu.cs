using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class MainMenu : MonoBehaviour
{
    public GameObject PopupPanel;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame

    public LevelManager levelManager; // Reference to your LevelManager script

    public void NewGame()
    {
        PlayerPrefs.DeleteAll(); // Reset player progress
        SceneManager.LoadScene("StageSelect");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Load()
    {
        // Check if there is any saved PlayerPrefs data
        if (PlayerPrefs.HasKey("Level 1_score")) // Check for any key that indicates saved data
        {
            // Load the stage menu scene
            SceneManager.LoadScene("StageSelect");
        }
        else
        {
            PopupPanel.gameObject.SetActive(true);
            //PopupPanel.ShowPopup();
            // No saved data, show a message or perform another action
            Debug.Log("No saved data found. Start a new game instead.");
            

        }
    }

    public void closePopup()
    {
        PopupPanel.gameObject.SetActive(false);
    }


    

   
    


public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}

