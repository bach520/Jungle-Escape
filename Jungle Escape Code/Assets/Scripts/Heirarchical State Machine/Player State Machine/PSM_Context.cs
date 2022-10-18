using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Context : MonoBehaviour
{
    #region state variables
    PSM_Abstract _currentState;
    PSM_StateFactory _stateFactory;
    [SerializeField]
    Player _user;
    Animator _userAnimator;
    Transform _playerTransform;
    [SerializeField]
    private float _jumpHeight = 3;
    private float _xAxisInfo;
    private bool _behindBush = false;
    private bool _isGrounded = true;
    private Collider2D _collider;
    private Rigidbody2D _playerRigidBody;
    private bool inWater = false;
    #endregion   

    private void Awake() 
    {
        _stateFactory = new PSM_StateFactory(this, _user.PlayerInventory.WeaponUsed);
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _userAnimator = GetComponent<Animator>();
        _playerTransform = GetComponent<Transform>();
        _collider = GetComponent<Collider2D>();
        _currentState = _stateFactory.Grounded();
        _currentState.EnterState();
    }
    
    // Run the updates from the superStates
    void Update()
    {
        _stateFactory.UpdateWeapon(_user.PlayerInventory.WeaponUsed);
        _currentState.UpdateStates();
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bush"))
        {
            _behindBush = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bush"))
        {
            _behindBush = false;
        }
    }

    #region unusedSoFar
    private void OnEnable() 
    {
        // may not need
    }
    private void OnDisable() 
    {
        // may not need
    }
    #endregion

    #region set/get    
    public PSM_Abstract CurrentState {get{return _currentState;} set{_currentState = value;}}
    public Player User{get{return _user;}}
    public Animator Anim{get{return _userAnimator;}}
    public Rigidbody2D RBody{get{return _playerRigidBody;}}
    public Transform PlayerTransform{get{return _playerTransform;}}
    public float xAxisInfo{get{return _xAxisInfo;} set{_xAxisInfo = value;}}
    public float JumpHeight{get{return _jumpHeight;} set{_jumpHeight = value;}}
    public bool IsGrounded{get{return _isGrounded;} set{_isGrounded = value;}}
    public bool BehindBush{get{return _behindBush;}}
    public bool InWater{get{return inWater;}}
    #endregion
}
