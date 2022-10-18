using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Character
{
#region variables
    [SerializeField] private InventoryObject playerInventory;

    [SerializeField] private SpriteRenderer spearSprite;

    [SerializeField] private SpriteRenderer dartSprite;

    [SerializeField] private PSM_Context context;

    [SerializeField] private Texture2D crossHair;
    [SerializeField] private ScoreKeeper playerScore;

    private CursorMode cursorMode = CursorMode.Auto;

    private Vector2 hotSpot;

    private Vector2 dartStart;

    //private Vector2 mousePosition;
    private float healMultiplier = 1;

    private bool inWatter = false;

    private bool _isHidden = false;
    private bool deathStarted = false;
    #endregion


    protected override void Start()
    {
        playerInventory.WeaponUsed = (WeaponBase)playerInventory.ItemsEquipped[0];
        context = GetComponent<PSM_Context>();
        base.Start();
        Physics2D.IgnoreLayerCollision(3, 7);
        dartSprite.enabled = false;
        hotSpot = new Vector2(crossHair.width / 2, crossHair.height / 2);
        Cursor.SetCursor(crossHair, hotSpot, cursorMode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        WeaponUpdate();
        PlayAudio();
        if(characterStats.GetCurrentHealth() <= 0 && deathStarted == false)
        {
            deathStarted = true;
            StartCoroutine("PlayerDeath");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.GetComponent<InteractItem>().PlayAudio(GetComponent<AudioSource>());
            playerInventory.AddItem(other.GetComponent<InteractItem>().item);
            Destroy(other.gameObject);
        }
    }

    private void WeaponUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!playerInventory.SlotEmpty(0))
            {
                playerInventory.WeaponUsed = (WeaponBase)playerInventory.GetItem(0);
                dartSprite.enabled = false;
                spearSprite.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GetComponent<PlayerAttack>().ChangeWeapon(playerInventory.WeaponUsed);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(!playerInventory.SlotEmpty(1))
            {
                playerInventory.WeaponUsed = (WeaponBase)playerInventory.GetItem(1);
                dartSprite.enabled = true;
                spearSprite.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GetComponent<PlayerAttack>().ChangeWeapon(playerInventory.WeaponUsed);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(!playerInventory.SlotEmpty(3))
            {
                HealingFruit fruit = (HealingFruit)playerInventory.GetItem(3);
                characterStats.AddHealth(fruit.useItem() * healMultiplier);
                playerInventory.RemoveItem(3);
            }
        }
    }

    private void PlayAudio()
    {
        if(characterStats.playAudio)
        {
            sound.clip = audioFiles[1];
            sound.Play();
            characterStats.playAudio = false;
        }
    }

    private IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(2.0f);
        GetComponent<SwitchScene>().Level = "Main Menu";
        GetComponent<SwitchScene>().GoToLevel();
    }

    #region set/get
    public StatsObject StatsObject{get{return characterStats;}}
    public InventoryObject PlayerInventory{get{return playerInventory;}}
    public Vector2 DartStart{get{return dartStart;}}
    public bool IsHidden{ get { return _isHidden; } set { _isHidden = value; } }
    public bool InWatter{get{return inWatter;}}
    #endregion

    #region may delete
    //public SpriteRenderer SRenderer{get{return sRenderer;}}
    //public SpriteRenderer SpearRenderer{get{return spearSprite;}}
    //public Texture2D CrossHair{get{return crossHair;}}
    #endregion
}
