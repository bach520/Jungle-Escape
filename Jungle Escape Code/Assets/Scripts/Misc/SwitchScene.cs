using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private ScoreKeeper score;   
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            score.SetScore();
            StartCoroutine("Wait");
        }
    }

    public void OnButtonClicked()
    {
        GoToLevel();
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }

    public string Level{set{nextLevel = value;}}

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
        GoToLevel();
    }
}
