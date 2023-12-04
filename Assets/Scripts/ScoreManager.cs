using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameManagerScript gameInfo;

    public Text highScoreText;

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highScoreText.text = "HighScore: 0";
    }
}
