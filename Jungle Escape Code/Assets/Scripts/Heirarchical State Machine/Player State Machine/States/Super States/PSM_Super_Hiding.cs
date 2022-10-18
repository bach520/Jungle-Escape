using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Super_Hiding : PSM_Abstract
{
    public PSM_Super_Hiding(PSM_Context context, PSM_StateFactory factory): base(context, factory)
    {
        ISSuper = true;
        InitializeSubState();
    }    
    public override void EnterState()
    {
        CurrentContext.Anim.SetBool("isCrouching", true);
    }
    public override void UpdateState()
    {
        SetHiding(CurrentContext.BehindBush);
        CheckSwitchState();
    }
    public override void ExitState()
    {
        SetHiding(false);
        CurrentContext.Anim.SetBool("isCrouching", false);
    }
    public override void CheckSwitchState()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {
        SetSubState(Factory.Idle());
    }

    public void SetHiding(bool behindBush)
    {
        CurrentContext.User.IsHidden = behindBush;
        if(CurrentContext.User.IsHidden)
        {
            CurrentContext.User.GetComponent<SpriteRenderer>().color = new Color( 1, 1, 1, 0.5f);
        }
        else
        {
            CurrentContext.User.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        }
    }
}
