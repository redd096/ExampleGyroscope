using redd096;
using UnityEngine;

public class GyroState : PlayerState
{
    public GyroState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Execution()
    {
        base.Execution();

        RotateOnGyroScope();
    }

    void RotateOnGyroScope()
    {
        //rotate
        player.GyroRotate(Input.gyro.attitude);
    }
}
