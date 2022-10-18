using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSM_Sub_Walk : PSM_Abstract
{
    public PSM_Sub_Walk(PSM_Context context, PSM_StateFactory factory):base(context, factory)
    {
        ISSuper = false;
    }

    
    public override void EnterState()
    {
        CurrentContext.Anim.SetBool("isWalking", true);
    }
    public override void UpdateState()
    {
        Move();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        // is walking is switched in Sub_Idle
    }
    public override void CheckSwitchState()
    {
        if(CurrentContext.xAxisInfo == 0f)
        {
            ExitState();
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState()
    {
        // sub states dont get this
    }

    private void Move()
    {
        CurrentContext.xAxisInfo = Input.GetAxisRaw("Horizontal");
        CurrentContext.PlayerTransform.position += new Vector3(CurrentContext.xAxisInfo, 0f,0f) * CurrentContext.User.StatsObject.GetSpeed() * Time.deltaTime;
        AnimateSprite();
    }
    private void AnimateSprite()
    {
        if(CurrentContext.xAxisInfo > 0)
        {
            CurrentContext.Anim.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(CurrentContext.xAxisInfo < 0)
        {
            CurrentContext.Anim.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            SwitchState(Factory.Idle());
        }
    }
    
}
