using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] SwitchScene switchScene;
    [SerializeField] FinalBossManager finalBoss;
    bool bossDie;
    // Start is called before the first frame update
    void Start()
    {
        bossDie = finalBoss.die;
    }

    // Update is called once per frame
    void Update()
    {
        bossDie = finalBoss.die;
        Debug.Log("Boss die: " + bossDie);
        if (bossDie) StartCoroutine(GoToGameOverMenu());
    }
    IEnumerator GoToGameOverMenu()
    {
        Debug.Log("Go to game over");
        yield return new WaitForSeconds(2f);
        switchScene.GoToLevel();
    }
}
