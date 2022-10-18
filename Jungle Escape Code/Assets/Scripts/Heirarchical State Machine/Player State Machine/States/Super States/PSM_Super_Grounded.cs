using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Super_Grounded : PSM_Abstract
{
    public PSM_Super_Grounded(PSM_Context context, PSM_StateFactory factory): base(context, factory)
    {
        ISSuper = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        CurrentContext.Anim.SetBool("isGrounded", true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        // exit state is not needed here because jump is the only state that will set isGrounded to false
    }
    public override void CheckSwitchState()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            ExitState();
            SwitchState(Factory.Jump());
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            ExitState();
            SwitchState(Factory.Sneak());
        }
    }

    public override void InitializeSubState()
    {
        SetSubState(Factory.Idle());
        CurrentSubState.EnterState();
    }
}
