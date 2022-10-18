public class PSM_StateFactory
{
    PSM_Context _context;
    WeaponBase _playerWeapon;

    public PSM_StateFactory(PSM_Context context, WeaponBase weapon)
    {
        _context = context;
        _playerWeapon = weapon;
    }
    // function for each state
    public PSM_Abstract Idle()
    {
        return new PSM_Sub_Idle(_context, this);
    }

    public PSM_Abstract Walk()
    {
        return new PSM_Sub_Walk(_context, this);
    }

    public PSM_Abstract Jump()
    {
        return new PSM_Super_JumpState(_context, this);
    }

    public PSM_Abstract Grounded()
    {
        return new PSM_Super_Grounded(_context, this);
    }

    public PSM_Abstract Sneak()
    {
        return new PSM_Super_Hiding(_context, this);
    }
    public PSM_Abstract Swim()
    {
        return new PSM_Super_Swimming(_context, this);
    }

    public void UpdateWeapon(WeaponBase weapon)
    {
        if(_playerWeapon != weapon)
        {
            _playerWeapon = weapon;
        }
    }

    public WeaponBase UsedWeapon{get{return _playerWeapon;}}
}
