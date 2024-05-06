﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    int sceneID;

    void Start()
    {
        // Get the build index of the current scene when the script starts
        sceneID = SceneManager.GetActiveScene().buildIndex;
    }

    public void StageClear()
    {
        // Increment the sceneID by 1 and set it as the score
        PlayerPrefs.SetInt("SCORE", sceneID + 1);

        // Save the score to PlayerPrefs
        PlayerPrefs.Save();
    }
}
