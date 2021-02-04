using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text _text;

    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.SetText("00:00:00");
    }

    // Update is called once per frame
    void Update()
    {
        float timeRaw = Time.timeSinceLevelLoad;
        int displayHours = (int)timeRaw / 3600;
        int displayMinutes = ((int)timeRaw % 3600) / 60;
        int displaySeconds = ((int)timeRaw % 3600) % 60;
        _text.SetText(String.Format("{0:00}:{1:00}:{2:00}", displayHours, displayMinutes, displaySeconds));
    }
}
