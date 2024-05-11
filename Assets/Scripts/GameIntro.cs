using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    public GameObject GameIntro;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Level 1_score")) // Check for any key that indicates saved data
        {
            GameIntro.gameObject.SetActive(false);
        }
        else
        {
            GameIntro.gameObject.SetActive(true);



        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openPopup()
    {
        GameIntro.gameObject.SetActive(true);
    }


    public void closePopup()
    {
        GameIntro.gameObject.SetActive(false);
    }
}
