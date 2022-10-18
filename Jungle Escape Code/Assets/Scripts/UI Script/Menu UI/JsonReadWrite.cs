using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonReadWrite : MonoBehaviour
{
    public TextAsset textJson;
    public HighScore topThree = new HighScore();

    public void Load() {
        topThree = JsonUtility.FromJson<HighScore>(textJson.text);
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(topThree, true);
        for(int i = 0; i < topThree.highScores.Length; i++)
        {
            Debug.LogWarning(topThree.highScores[i].score);
        }
        File.WriteAllText(Application.dataPath + textJson.name, json);
    }

    public void AddPlayer(Score newScore)
    {
        for(int i = 0; i < topThree.highScores.Length; i++)
        {
            Score previousScore;
            if(newScore.score > topThree.highScores[i].score)
            {
                previousScore = topThree.highScores[i];
                topThree.highScores[i] = newScore;
                AddPlayer(previousScore);
                return;
            }
        }
    }
}

[System.Serializable]
public class Score
{
    public string name;
    public int score;

    public Score(string _name, int _Score)
    {
        name = _name;
        score = _Score;
    }
}

[System.Serializable]
public class HighScore
{
    public Score[] highScores;
}
