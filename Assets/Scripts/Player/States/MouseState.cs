using redd096;
using UnityEngine;

public class MouseState : PlayerState
{
    Camera cam;

    public MouseState(StateMachine stateMachine) : base(stateMachine)
    {
        //get camera
        cam = Camera.main;
    }

    public override void Execution()
    {
        base.Execution();

        RotateOnMouse();
    }

    void RotateOnMouse()
    {
        //from screen to viewport, then remap from (0, 1) to limit axis
        Vector3 inputPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        inputPosition.x = player.Remap(inputPosition.x, 0, 1, true);
        inputPosition.y = player.Remap(inputPosition.y, 0, 1, false);

        //set euler angles
        player.Rotate(inputPosition);
    }
}
