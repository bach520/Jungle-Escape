using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI textmesh;
    public float minutes = 0;
    public float seconds = 0;
    private bool countTime = true;
    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponentInChildren<TextMeshProUGUI>();
        textmesh.text = "00:00";
        countTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
        textmesh.text = minutes + ":" + Mathf.Round(seconds) % 60;
    }

    private void TimeUpdate()
    {
        if(!countTime)
        {
            return;
        }
        seconds += Time.deltaTime;
        if(seconds >= 59)
        {
            minutes += 1;
        }
    }
    public void StopTimer()
    {
        countTime = false;
    }
}
