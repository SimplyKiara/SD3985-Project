using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string LevelText;
        public int UnLocked;
        public bool IsInteractable;

        public Button.ButtonClickedEvent OnClickEvent;
    }
    public GameObject levelbutton;
    public Transform Spacer;

    public List<Level> LevelList;
    


    // Start is called before the first frame update
    void Start()
    {
        FillList();
    }

    void FillList()
    {
        foreach(var  level in LevelList)
        {
            GameObject newbutton = Instantiate(levelbutton) as GameObject;
            LevelButton button = newbutton.GetComponent<LevelButton>();
            button.LevelText.text = level.LevelText;

            newbutton.transform.SetParent(Spacer    );
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
