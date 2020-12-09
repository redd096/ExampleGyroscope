using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using redd096;

public enum PlayerCommands
{
    analog, gyro, accel, mouse
}

public class Player : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] Transform objectToRotate = default;
    [SerializeField] float rotationHandler = 0.5f;

    [Header("Limits")]
    [SerializeField] float xAxis = 50;
    [SerializeField] float zAxis = 50;

    [Header("Analog")]
    [SerializeField] UnityEngine.UI.GraphicRaycaster raycaster = default;

    Camera cam;
    PlayerCommands commands;
    EventSystem eventSystem;
    RectTransform draggedObject;

    void Start()
    {
        cam = Camera.main;
        eventSystem = FindObjectOfType<EventSystem>();

        AddEvents();

        enabled = false;
    }

    void OnDestroy()
    {
        RemoveEvents();
    }

    void Update()
    {
        //if press escape or start, pause or resume game
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (Time.timeScale <= 0)
                SceneLoader.instance.ResumeGame();
            else
                SceneLoader.instance.PauseGame();
        }

        //use different commands
        switch (commands)
        {
            case PlayerCommands.analog:
                CheckAnalog();
                break;
            case PlayerCommands.gyro:
                RotateOnGyroScope();
                break;
            case PlayerCommands.accel:
                RotateOnAccelerometer();
                break;
            case PlayerCommands.mouse:
                RotateOnMouse();
                break;
        }
    }

    #region events

    void AddEvents()
    {
        GameManager.instance.levelManager.onStartGame += OnStartGame;
    }

    void RemoveEvents()
    {
        GameManager.instance.levelManager.onStartGame -= OnStartGame;
    }

    void OnStartGame()
    {
        //enable player
        enabled = true;
    }

    #endregion

    #region commands API

    void CheckAnalog()
    {
        //if mouse click or touching screen
        if (Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0)
        {
            //set pointer event on input position, then raycast and get hits
            PointerEventData pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = InputPosition();

            List<RaycastResult> hits = new List<RaycastResult>();
            raycaster.Raycast(pointerEvent, hits);

            //unique possible hit is analog area, if hit start drag
            if (draggedObject == null && hits.Count > 0)
            {
                draggedObject = hits[0].gameObject.GetComponent<RectTransform>();
            }

            //if drag, move analog
            if(draggedObject)
            {
                GameManager.instance.uiManager.AnalogPosition(pointerEvent.position);
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

    void RotateOnAnalog()
    {
        //if no hit, reset euler angles
        if(draggedObject == null)
        {
            Rotate(Vector3.zero);
            return;
        }

        //get anchored position of analog, then remap from (-areaSize, areaSize) to limit axis
        //(analog anchored position move from -area.sizeDelta/2 to area.sizeDelta/2)
        Vector3 inputPosition = GameManager.instance.uiManager.GetAnalogAnchoredPosition();
        inputPosition.x = Remap(inputPosition.x, -draggedObject.sizeDelta.x /2, draggedObject.sizeDelta.x /2, -xAxis, xAxis);
        inputPosition.y = Remap(inputPosition.y, -draggedObject.sizeDelta.y /2, draggedObject.sizeDelta.y /2, -zAxis, zAxis);

        //set euler angles
        Rotate(inputPosition);
    }

    void RotateOnGyroScope()
    {
        //set rotation base on gyro
        objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, Input.gyro.attitude, Time.deltaTime * rotationHandler);

        GameManager.instance.uiManager.SetText(Input.gyro.attitude.ToString());
    }

    void RotateOnAccelerometer()
    {
        //use acceleration, then remap from (-1, 1) to limit axis
        Vector3 inputPosition = Input.acceleration;
        inputPosition.x = Remap(inputPosition.x, -1, 1, -xAxis, xAxis);
        inputPosition.y = Remap(inputPosition.y, -1, 1, -zAxis, zAxis);

        //set euler angles
        Rotate(inputPosition);
    }

    void RotateOnMouse()
    {
        //from screen to viewport, then remap from (0, 1) to limit axis
        Vector3 inputPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        inputPosition.x = Remap(inputPosition.x, 0, 1, -xAxis, xAxis);
        inputPosition.y = Remap(inputPosition.y, 0, 1, -zAxis, zAxis);

        //set euler angles
        Rotate(inputPosition);
    }

    #endregion

    #region public API

    public void SetCommands(int input)
    {
        commands = (PlayerCommands)input;
    }

    #endregion

    #region private API

    Vector3 InputPosition()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.GetTouch(0).position;
#else
        return Input.mousePosition;
#endif
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Rotate(Vector3 inputPosition)
    {
        //set euler angles
        Vector3 euler = new Vector3(inputPosition.y, 0, -inputPosition.x);
        objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, Quaternion.Euler(euler), Time.deltaTime * rotationHandler);

        GameManager.instance.uiManager.SetText(euler.ToString());
    }

    #endregion
}
