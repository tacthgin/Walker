
using UnityEngine;

public class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (parent.Target != null)
        {
            parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Target.position, parent.Speed * Time.deltaTime);
        }else
        {
            parent.Direction = Vector2.zero;
            parent.ChangeState(new IdleState());
        }
    }
}