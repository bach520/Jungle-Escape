using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Sub_Idle : PSM_Abstract
{
    public PSM_Sub_Idle(PSM_Context context, PSM_StateFactory factory)
    :base(context, factory)
    {
        ISSuper = false;
    }

    
    public override void EnterState()
    {
        CurrentContext.Anim.SetBool("isWalking", false);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        // set stats back to normal
    }
    public override void CheckSwitchState()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            ExitState();
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState()
    {
        
    }
}
