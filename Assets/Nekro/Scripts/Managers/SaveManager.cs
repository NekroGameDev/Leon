using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.Save();
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }
}