﻿using UnityEngine;

public class IdleState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        this.parent.Reset();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        //如果玩家靠近，切到跟随状态
        if (parent.MyTarget != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}