using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string LevelText;
        public int UnLocked;
        public bool IsInteractable;

        //public Button.ButtonClickedEvent OnClickEvent;
    }
    public GameObject levelbutton;
    public Transform Spacer;

    public List<Level> LevelList;
    


    // Start is called before the first frame update
    void Start()
    {
        //DeleteAll();
        FillList();
    }

    void FillList()
    {
        foreach (var level in LevelList)
        {
            GameObject newbutton = Instantiate(levelbutton) as GameObject;
            LevelButton button = newbutton.GetComponent<LevelButton>();
            button.LevelText.text = level.LevelText;

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text) == 1)
            {
                level.UnLocked = 1;
                level.IsInteractable = true;

            }

       
            button.unlocked = level.UnLocked;
            button.GetComponent<Button>().interactable = level.IsInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => loadLevels("Level" + button.LevelText.text));

            if (level.UnLocked == 0)
            {
                button.Lock.SetActive(true);
                button.ButtonFrame.SetActive(false);
                button.LevelText.text = "";
                
            }
            if (level.UnLocked == 1)
            {
                button.Lock.SetActive(false);
                button.ButtonFrame.SetActive(true);
                button.LevelText.text = level.LevelText;

            }

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") == 0)
            {
                button.Star1.SetActive(false);
                button.Star2.SetActive(false);
                button.Star3.SetActive(false);
            }

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") == 1)
            {
                button.Star1.SetActive(true);
            }

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") == 2)
            {
                button.Star1.SetActive(true);
                button.Star2.SetActive(true);
            }

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") == 3)
            {
                button.Star1.SetActive(true);
                button.Star2.SetActive(true);
                button.Star3.SetActive(true);
            }

            newbutton.transform.SetParent(Spacer);
        }
        SaveAll();
        

    }

    void SaveAll()
    {
//        if (PlayerPrefs.HasKey("Level"))
//        {
//            return;
//        }
//        else
        {
            GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
            foreach(GameObject buttons in allButtons)
            {
                LevelButton button = buttons.GetComponent<LevelButton>();
                PlayerPrefs.SetInt("Level" + button.LevelText.text, button.unlocked);
            }
        }
    }
    // Update is called once per frame
    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void loadLevels(string value)
    {
        SceneManager.LoadScene(value);
    }
}
