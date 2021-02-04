using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] AudioSource BGMusic;
    [SerializeField] AudioSource Ambient;
    public TMP_Text SurviveTime;
    private bool timeSet = false;

    public void Setup()
    {
        if (!timeSet)
        {
            float timeRaw = Time.timeSinceLevelLoad;
            int displayHours = (int)timeRaw / 3600;
            int displayMinutes = ((int)timeRaw % 3600) / 60;
            int displaySeconds = ((int)timeRaw % 3600) % 60;

            SurviveTime.SetText("You survived for: " + String.Format("{0:00}:{1:00}:{2:00}", displayHours, displayMinutes, displaySeconds));
            gameObject.SetActive(true);
            BGMusic.Stop();
            Ambient.Stop();
            timeSet = true;
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Level");
    }
}
