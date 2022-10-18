using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum YN
{
    YES,
    NO
}

public class LevelStart : MonoBehaviour
{
    #region variables
    [SerializeField]
    Player player;
    [SerializeField]
    private WeaponBase BlowDart;
    [SerializeField]
    private Transform playerStart;
    [SerializeField]
    private YN DartLevel;
    [SerializeField]
    private YN ResetDarts;
    [SerializeField]
    private YN resetScore;
    #endregion

    private void Start()
    {
        player.transform.position = playerStart.position;
        if(DartLevel == YN.NO)
        {
            player.PlayerInventory.TotalDarts = 0;
            player.PlayerInventory.ItemsEquipped[1] = null;
        }
        else if(DartLevel == YN.YES)
        {
            player.PlayerInventory.ItemsEquipped[1] = Instantiate(BlowDart);
        }
        if(ResetDarts == YN.YES)
        {
            player.PlayerInventory.TotalDarts = 0;
        }
        if(resetScore == YN.YES)
        {
            player.PlayerInventory.PlayerScore = 0;
        }
    }
}
