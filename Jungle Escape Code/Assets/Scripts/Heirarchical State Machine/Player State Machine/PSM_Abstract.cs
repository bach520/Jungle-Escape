public abstract class PSM_Abstract
{
    #region variables
    private PSM_Context _context;
    private PSM_StateFactory _factory;
    private PSM_Abstract _currentSubState;
    private PSM_Abstract _currentSuperState;
    private bool isSuperstate = false;
    #endregion

    public PSM_Abstract(PSM_Context context, PSM_StateFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    // functions for child classes
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();

    // general functions
    public void UpdateStates()
    {
        UpdateState();
        if(_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }
    protected void SwitchState(PSM_Abstract newState)
    {
        ExitState();
        newState.EnterState();

        // dont need these for finite

        if(isSuperstate)
        {
            _context.CurrentState = newState;
        }
        else if(_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    // dont need this

    protected void SetSuperState(PSM_Abstract newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    // dont need this

    protected void SetSubState(PSM_Abstract newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this); // passes the superstate to the substate
    }

    // getters
    protected PSM_Abstract CurrentState{get{return _currentSuperState;} set{_currentSuperState = value;}}
    protected PSM_Context CurrentContext{get{return _context;}}
    protected PSM_StateFactory Factory{get{return _factory;}}
    protected PSM_Abstract CurrentSubState{get{return _currentSubState;}}
    protected bool ISSuper{get{return isSuperstate;} set{isSuperstate = value;}}
}
