using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region variables
    [SerializeField]
    protected Animator _animator;
    [SerializeField]
    protected StatsObject characterStats;
    [SerializeField]
    protected List<AudioClip> audioFiles;
    protected AudioSource sound;
    protected SpriteRenderer sRenderer;
    #endregion

    protected virtual void Start() 
    {
        _animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        sRenderer.sprite = characterStats.GetSprite();
        characterStats.SetStats();
    }

    #region set/get    
    public StatsObject Stats{get{return characterStats;}}
    public List<AudioClip> AudioList{get{return audioFiles;}}
    #endregion
}
