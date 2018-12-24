using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public MenuScript menu;
    public enum GameState { Intro, Reading, Path, Sequence, End};
    private GameState state = GameState.Intro;
    private HeartBeatController hbController;

    public IActivity currentActivity;

    

    private void Awake()
    {
        singleton = this;
        //UnityEngine.XR.XRSettings.enabled = false;

        hbController = GetComponent<HeartBeatController>();
        menu.buttonsPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //UnloadAllStates();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hbController.TriggerBeat();
        }
    }

    public static GameManager GetGameManager()
    {
        return singleton;
    }

    public GameState GetState()
    {
        return state;
    }

    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Intro:
                break;
            case GameState.Reading:
                LoadReadingState();
                break;
            case GameState.Path:
                LoadPathState();
                break;
            case GameState.Sequence:
                LoadSequenceState();
                break;
            case GameState.End:
                break;
            default:
                break;
        }
    }

    private void UnloadAllStates()
    {
        UnloadPathState();
        UnloadSequenceState();
        UnloadReadingState();
    }

    private void UnloadCurrentState()
    {
        switch (state)
        {
            case GameState.Intro:
                menu.buttonsPanel.gameObject.SetActive(true);
                break;
            case GameState.Reading:
                UnloadReadingState();
                break;
            case GameState.Path:
                UnloadPathState();
                break;
            case GameState.Sequence:
                UnloadSequenceState();
                break;
            case GameState.End:
                break;
            default:
                break;
        }
    }

    private void UnloadSequenceState()
    {
        menu.sequencePanelTransform.gameObject.SetActive(false);
    }

    private void UnloadPathState()
    {
        menu.pathPanelTransform.gameObject.SetActive(false);
    }

    private void UnloadReadingState()
    {
        menu.readingPanelTransform.gameObject.SetActive(false);
    }

    public void LoadReadingState()
    {
        UnloadCurrentState();
        state = GameState.Reading;
        LoadScene("ReadingScene");
        menu.readingPanelTransform.gameObject.SetActive(true);
        
    }

    public void LoadSequenceState()
    {
        UnloadCurrentState();
        state = GameState.Sequence;
        LoadScene("SequenceScene");
        menu.sequencePanelTransform.gameObject.SetActive(true);
    }

    public void LoadPathState()
    {
        UnloadCurrentState();
        state = GameState.Path;
        LoadScene("PathScene");
        menu.pathPanelTransform.gameObject.SetActive(true);
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log("Loading " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void StartActivity()
    {
        Debug.Log("Starting activity");
        currentActivity.StartActivity();

    }

    public void PauseActivity()
    {
        Debug.Log("Pausing activity");
        currentActivity.PauseActivity();
    }

    public void StopActivity()
    {
        Debug.Log("Stopping activity");
        currentActivity.StopActivity();
    }

    public void SetCurrentActitivy(IActivity activity)
    {
        currentActivity = activity;
    }
}
