using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] GameObject[] names;
    [SerializeField] GameObject[] scores;
    Score[] savedScores;

    private void Start()
    {
        GetComponent<JsonReadWrite>().Load();
        savedScores = GetComponent<JsonReadWrite>().topThree.highScores;

        for(int i = 0; i < names.Length; i++)
        {
            names[i].GetComponent<TextMeshProUGUI>().text = savedScores[i].name;
            scores[i].GetComponent<TextMeshProUGUI>().text = savedScores[i].score.ToString();
        }
    }
}
