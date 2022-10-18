using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Super_Swimming : PSM_Abstract
{
    float waterPull = 3.0f;
    public PSM_Super_Swimming(PSM_Context context, PSM_StateFactory factory): base(context, factory)
    {
        ISSuper = true;
        InitializeSubState();
    }
    public override void CheckSwitchState()
    {
        if(!CurrentContext.InWater)
        {
            ExitState();
            SwitchState(Factory.Grounded());
        }
    }

    public override void EnterState()
    {
        CurrentContext.User.GetComponent<Rigidbody2D>().mass = waterPull;
        CurrentContext.Anim.SetBool("isGrounded", true);
    }

    public override void ExitState()
    {
        CurrentContext.User.GetComponent<Rigidbody2D>().mass = 1f;
    }

    public override void InitializeSubState()
    {
        SetSubState(Factory.Idle());
        CurrentSubState.EnterState();
    }

    public override void UpdateState()
    {
        // if the player is no longer in water set to grounded
        // if the player is under the water reduce oxygen
        // if the player is above water add to oxygen
        CheckSwitchState();
    }
}
