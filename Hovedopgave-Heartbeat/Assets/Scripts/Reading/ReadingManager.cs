using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadingManager : MonoBehaviour, IActivity
{

    public TextAsset textAsset;
    public TextMeshProUGUI textMeshPro;
    public float cursorSpeed = 1;
    public Color readingColor1 = Color.blue;
    public Color readingColor2 = Color.red;
    public Color neutralColor = Color.white;
    public enum PLAYERS {PLAYER1, PLAYER2 };
    public PLAYERS currentPlayer = PLAYERS.PLAYER1;
    public int wordBuffer = 2;

    private int readingIndex = -1;
    private float timeCounter = 0;

    private int currentLine = 0;
    private int charCounter = 0;

    public float currentCharPosition;

    public Camera cam;
    public float camLerpSpeed = 1.0f;
    int maxCharCount;

    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        string s = textAsset.text.Replace(Environment.NewLine, "");
        textMeshPro.text = s;

        GameManager.GetGameManager().SetCurrentActitivy(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 txtPos = textMeshPro.GetComponent<RectTransform>().localPosition;

        float lerpedY = Mathf.Lerp(txtPos.y, -currentCharPosition, Time.deltaTime * camLerpSpeed);
        textMeshPro.GetComponent<RectTransform>().localPosition = new Vector3(txtPos.x, lerpedY, txtPos.z);

        if (!isRunning)
        {
            return;
        }

        maxCharCount = textMeshPro.textInfo.characterCount;

        if (readingIndex >= maxCharCount)
        {
            isRunning = false;
        }

        timeCounter += Time.deltaTime * cursorSpeed;

        if(timeCounter >= 1)
        {
            timeCounter = 0;
            readingIndex++;
            /*
            ColorCharacter(readingIndex, GetCurrentReadingColor());
            if(readingIndex > wordBuffer)
            {
                ColorCharacter(readingIndex- wordBuffer, neutralColor);
            }
            */


            ColorOneCharacter(readingIndex, GetCurrentReadingColor());



            //Vector3 des = Mathf.Lerp(textMeshPro.transform.position.y, textMeshPro.transform.position.y + 0.1f, 0.1f);

            if(currentLine < textMeshPro.textInfo.lineInfo.Length - 1)
            {
                int charsInLine = textMeshPro.textInfo.lineInfo[currentLine].characterCount;
                print("Characters in line: " + charsInLine + ", Current Line: " + currentLine + ", Current Char in Line: " + charCounter);
                charCounter++;
                if (charCounter >= charsInLine)
                {
                    charCounter = 0;
                    currentLine++;
                }

                currentCharPosition = GetCharPosition(readingIndex).y;
                print("Character position :" + currentCharPosition);
            }
            else
            {
                //End of the line budddy
            }
            
        }

        HandleInput();
        /*
        print("textMeshPro.textInfo.characterCount: " + textMeshPro.textInfo.characterCount);
        print("textMeshPro.textInfo.lineCount " + textMeshPro.textInfo.lineCount);
        print("textMeshPro.textInfo.wordCount " + textMeshPro.textInfo.wordCount);
        print("textMeshPro.textInfo.lineInfo[readingIndex].wordCount " + textMeshPro.textInfo.lineInfo[0].characterCount);
        print("Textbounds: " + textMeshPro.textBounds);
        */


        
    }

    public static void SetRect(RectTransform trs, float left, float top, float right, float bottom)
    {
        trs.offsetMin = new Vector2(left, bottom);
        trs.offsetMax = new Vector2(-right, -top);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPlayer = (currentPlayer == PLAYERS.PLAYER1) ? PLAYERS.PLAYER2 : PLAYERS.PLAYER1;
        }
    }

    private Vector3 GetCharPosition(int charIndex)
    {
        return textMeshPro.textInfo.characterInfo[charIndex].bottomLeft;
    }

    private void ColorOneCharacter(int charIndex, Color color)
    {
        print("Coloring index: " + charIndex);
        int meshIndex = textMeshPro.textInfo.characterInfo[charIndex].materialReferenceIndex;
        int vertexIndex = textMeshPro.textInfo.characterInfo[charIndex].vertexIndex;

        Color32[] vertexColors = textMeshPro.textInfo.meshInfo[meshIndex].colors32;
        vertexColors[vertexIndex + 0] = color;
        vertexColors[vertexIndex + 1] = color;
        vertexColors[vertexIndex + 2] = color;
        vertexColors[vertexIndex + 3] = color;

        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

    }

    private void ColorCharacter(int readingIndex, Color color)
    {
        TMP_WordInfo info = textMeshPro.textInfo.wordInfo[readingIndex];
        for (int i = 0; i < info.characterCount; ++i)
        {
            int charIndex = info.firstCharacterIndex + i;
            int meshIndex = textMeshPro.textInfo.characterInfo[charIndex].materialReferenceIndex;
            int vertexIndex = textMeshPro.textInfo.characterInfo[charIndex].vertexIndex;

            Color32[] vertexColors = textMeshPro.textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }

        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

    }

    private Color GetCurrentReadingColor()
    {
        if(currentPlayer == PLAYERS.PLAYER1)
        {
            return readingColor1;
        }
        else
        {
            return readingColor2;
        }
    }

    public void StartActivity()
    {
        isRunning = true;
    }

    public void PauseActivity()
    {
        isRunning = false;
    }

    public void StopActivity()
    {
        isRunning = false;
    }
}
