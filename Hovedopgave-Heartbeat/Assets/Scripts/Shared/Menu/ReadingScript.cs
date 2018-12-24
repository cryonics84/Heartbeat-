using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadingScript : MonoBehaviour
{
    public ReadingManager readingManager;

    public Text readingSpeedText;
    public Slider slider;
    private string defaultText;
    public Button startButton;

    void Start()
    {
        
    }

    void Update()
    {
        if (!readingManager)
        {
            readingManager = GameObject.Find("ReadingManager").GetComponent<ReadingManager>();

            if (readingManager)
            {
                defaultText = readingSpeedText.text;
                readingSpeedText.text = defaultText + slider.value.ToString();
            }
        }
    }

    public void SliderChanged()
    {
        Debug.Log("Changed slider value: " + slider.value);
        readingSpeedText.text = defaultText + slider.value.ToString();
        readingManager.cursorSpeed = slider.value;
    }

    public void PushedStart()
    {
        
    }
    
}
