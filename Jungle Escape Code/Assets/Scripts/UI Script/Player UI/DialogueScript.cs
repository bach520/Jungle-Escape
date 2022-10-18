using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueScript : MonoBehaviour
{
    #region variables
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Image npcImage; // for if there is a conversation with the boss at the end
    [SerializeField]
    private Image playerImage;
    [SerializeField]
    private TextMeshProUGUI textInBox;
    [SerializeField]
    private List<string> dialogue = new List<string>();
    private int index;
    #endregion

    void Start()
    {
        index = 0;
        textInBox.text = dialogue[index];
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(index == dialogue.Count - 1)
            {
                SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
            }
            else
            {
                textInBox.text = dialogue[++index];
            }
        }
    }
}
