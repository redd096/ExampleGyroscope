using redd096;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnalogState : PlayerState
{
    EventSystem eventSystem;
    RectTransform draggedObject;

    public AnalogState(StateMachine stateMachine) : base(stateMachine)
    {
        //get event system
        eventSystem = Object.FindObjectOfType<EventSystem>();
    }

    public override void Execution()
    {
        base.Execution();

        CheckAnalog();
    }

    #region private API

    void CheckAnalog()
    {
        //if mouse click or touching screen
        if (Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0)
        {
            //raycast
            List<RaycastResult> hits = RaycastHits();

            //unique possible hit is analog area, if hit start drag
            if (draggedObject == null && hits.Count > 0)
            {
                draggedObject = hits[0].gameObject.GetComponent<RectTransform>();
            }

            //if drag, move analog
            if (draggedObject)
            {
                GameManager.instance.uiManager.AnalogPosition(Utility.InputPosition());
            }
        }
        //else, stop drag
        else if (draggedObject)
        {
            draggedObject = null;
        }

        //if no drag reset analog position
        if (draggedObject == null)
            GameManager.instance.uiManager.ResetAnalogPosition();

        //rotate
        RotateOnAnalog();
    }

    List<RaycastResult> RaycastHits()
    {
        //set pointer event on input position
        PointerEventData pointerEvent = new PointerEventData(eventSystem);
        pointerEvent.position = Utility.InputPosition();

        //then raycast and get hits
        List<RaycastResult> hits = new List<RaycastResult>();
        player.Raycaster.Raycast(pointerEvent, hits);

        return hits;
    }

    #endregion

    void RotateOnAnalog()
    {
        //if no hit, reset euler angles
        if (draggedObject == null)
        {
            player.Rotate(Vector3.zero);
            return;
        }

        //get anchored position of analog, then remap from (-areaSize, areaSize) to player limit axis
        //(analog anchored position move from -area.sizeDelta/2 to area.sizeDelta/2)
        Vector3 inputPosition = GameManager.instance.uiManager.GetAnalogAnchoredPosition();
        inputPosition.x = player.Remap(inputPosition.x, -draggedObject.sizeDelta.x / 2, draggedObject.sizeDelta.x / 2, true);
        inputPosition.y = player.Remap(inputPosition.y, -draggedObject.sizeDelta.y / 2, draggedObject.sizeDelta.y / 2, false);

        //set euler angles
        player.Rotate(inputPosition);
    }
}
