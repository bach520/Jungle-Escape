using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private Timer timeScore;
    [SerializeField]private GameObject scoreActive;
    [SerializeField] private InventoryObject userInventory;
    [SerializeField] private TextMeshProUGUI timeScoreText;
    [SerializeField] private TextMeshProUGUI timeTotalText;
    [SerializeField] private TextMeshProUGUI coinTotalText;
    [SerializeField] private TextMeshProUGUI coinScoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI levelScoreText;
    
    private int totalScore;
    [SerializeField] private int levelScore;
    private JsonReadWrite saveLoad;
    private int coinTotal;
    private int coinMult = 4;
    private float timeMult = 0.25f;

    public void SetScore()
    {
        saveLoad = GetComponent<JsonReadWrite>();
        timeScore = GetComponentInChildren<Timer>();
        timeScore.StopTimer();

        levelScore =  GetTimeTotal(timeScore.minutes, timeScore.seconds) + GetCoinTotal();
        levelScoreText.text = levelScore.ToString();

        totalScore = levelScore + userInventory.PlayerScore;
        totalScoreText.text = totalScore.ToString();
        
        scoreActive.SetActive(true);
        userInventory.PlayerScore = totalScore;
        if(saveLoad.topThree.highScores[0].score <= 0)
        {
            saveLoad.Load();
        }
        saveLoad.AddPlayer(new Score(GetPlayerName(), totalScore));
        saveLoad.Save();
    }

    private int GetTimeTotal(float min, float sec)
    {
        int totalTimeScore = 0;
        totalTimeScore -= (int)(((min * 60) + sec) * timeMult);
        Debug.LogWarning(totalTimeScore + "Is passed");
        timeTotalText.text = (int)min + ":" + (int)(sec);
        timeScoreText.text = totalTimeScore.ToString();

        return totalTimeScore;
    }

    private int GetCoinTotal()
    {
        int coinTotal = userInventory.TotalCollectables * coinMult;
        coinTotalText.text = userInventory.TotalCollectables.ToString();
        coinScoreText.text = coinTotal.ToString();
        Debug.LogWarning("COINS: " + coinTotal);

        return coinTotal;
    }

    private string GetPlayerName()
    {
        string name = "test";
        Debug.Log("New name needed");

        return name;
    }

    public int Score{get{return totalScore;}}
}