using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Transform pathPanelTransform, sequencePanelTransform, readingPanelTransform;
    public ReadingScript readingScript;
    public SequenceScript sequenceScript;
    public PathScript pathscript;

    public Transform buttonsPanel;
    public Button startButton, pauseButton, stopButton;

    // Start is called before the first frame update
    void Start()
    {
        //readingScript.enabled = false;
        //sequenceScript.enabled = false;
        //pathscript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
