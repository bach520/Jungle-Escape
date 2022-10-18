using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Super_JumpState : PSM_Abstract
{
    private float yStart;
    public PSM_Super_JumpState(PSM_Context context, PSM_StateFactory factory): base(context, factory)
    {
        ISSuper = true;
        InitializeSubState();
    }

    
    public override void EnterState()
    {
        CurrentContext.Anim.SetBool("isGrounded", false);
        HandleJump();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        
    }
    public override void CheckSwitchState()
    {
        if(CurrentContext.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }
    
    public override void InitializeSubState()
    {
        SetSubState(Factory.Idle());
    }

    private void HandleJump()
    {
        CurrentContext.RBody.AddForce(new Vector2(0f, CurrentContext.JumpHeight), ForceMode2D.Impulse);
        CurrentContext.IsGrounded = false;
    }
}
