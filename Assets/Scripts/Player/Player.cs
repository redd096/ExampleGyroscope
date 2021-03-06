﻿using UnityEngine;
using redd096;

[SelectionBase]
public class Player : StateMachine
{
    #region variables

    [Header("Important")]
    [SerializeField] Transform objectToRotate = default;
    [SerializeField] float rotationHandler = 0.5f;

    [Header("Limits")]
    [SerializeField] float xAxis = 50;
    [SerializeField] float zAxis = 50;

    [Header("Analog")]
    [SerializeField] AnalogScript analogScript = default;
    [SerializeField] UnityEngine.UI.GraphicRaycaster raycaster = default;

    public AnalogScript AnalogScript => analogScript;
    public UnityEngine.UI.GraphicRaycaster Raycaster => raycaster;
    bool canMove;

    #endregion

    void Start()
    {
        AddEvents();

        //set object to rotate if not setted
        if (objectToRotate == null)
            objectToRotate = FindObjectOfType<GridBase>().transform;

        //by default is in analog state
        SetState(new AnalogState(this));
    }

    void OnDestroy()
    {
        RemoveEvents();
    }

    protected override void Update()
    {
        //use bool, because can change state also during timer before start game
        if (canMove)
        {
            base.Update();
        }

        //if press escape or start, pause or resume game
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (Time.timeScale <= 0)
                OldSceneLoader.instance.ResumeGame();
            else
                OldSceneLoader.instance.PauseGame();
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
        canMove = true;
    }

    #endregion

    #region public API

    public void SetCommands(int input)
    {
        //set state
        switch (input)
        {
            case 0:
                SetState(new AnalogState(this));
                break;
            case 1:
                SetState(new GyroState(this));
                break;
            case 2:
                SetState(new AccelState(this));
                break;
            case 3:
                SetState(new MouseState(this));
                break;
        }

        //active analog only on analog state
        raycaster.gameObject.SetActive(input == 0);
    }

    public void Rotate(Vector3 inputPosition)
    {
        //set euler angles
        Vector3 euler = new Vector3(inputPosition.y, 0, -inputPosition.x);
        objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, Quaternion.Euler(euler), Time.deltaTime * rotationHandler);

        //update UI
        GameManager.instance.uiManager.SetDebugText(euler.ToString());
    }

    public void GyroRotate(Quaternion inputRotation)
    {
        //set rotation base on gyro
        objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, inputRotation, Time.deltaTime * rotationHandler);

        //update UI
        GameManager.instance.uiManager.SetDebugText(inputRotation.ToString());
    }

    public float Remap(float value, float from, float to, bool useXAxis)
    {
        //use x or z axis
        float axis = useXAxis ? xAxis : zAxis;

        //remap value
        return value.Remap(from, to, -axis, axis);
    }

    #endregion
}
