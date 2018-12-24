using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PathScript : MonoBehaviour
{
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushedStart()
    {
        GameManager.GetGameManager().ChangeState(GameManager.GameState.Path);
    }
}
