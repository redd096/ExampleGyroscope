using redd096;
using UnityEngine;

public class AccelState : PlayerState
{
    public AccelState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Execution()
    {
        base.Execution();

        RotateOnAccelerometer();
    }

    void RotateOnAccelerometer()
    {
        //use acceleration, then remap from (-1, 1) to player axis
        Vector3 inputPosition = Input.acceleration;
        inputPosition.x = player.Remap(inputPosition.x, -1, 1, true);
        inputPosition.y = player.Remap(inputPosition.y, -1, 1, false);

        //rotate
        player.Rotate(inputPosition);
    }
}
