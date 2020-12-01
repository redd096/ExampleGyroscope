using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] Transform objectToRotate = default;
    [SerializeField] float rotationHandler = 5;
    [SerializeField] UnityEngine.UI.Text textToSet = default;

    [Header("Limits")]
    [SerializeField] float xAxis = 50;
    [SerializeField] float zAxis = 50;

    Camera cam;
    bool useGyro = true;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        //use gyro or accel
        if (useGyro)
        {
            RotateOnGyroScope();
        }
        else
        {
            RotateOnAccelerometer();
        }
#else
        RotateOnMouse();
#endif
    }

    #region pc API

    void RotateOnMouse()
    {
        //from screen to viewport, then remap from (0, 1) to limit axis
        Vector3 inputPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        inputPosition.x = Remap(inputPosition.x, 0, 1, -xAxis, xAxis);
        inputPosition.y = Remap(inputPosition.y, 0, 1, -zAxis, zAxis);

        //set euler angles
        Vector3 euler = new Vector3(inputPosition.y, 0, -inputPosition.x);
        objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, Quaternion.Euler(euler), Time.deltaTime * rotationHandler);

        textToSet.text = euler.ToString();
    }

    #endregion

    #region mobile API

    void RotateOnGyroScope()
    {
        //set rotation base on gyro
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, Input.gyro.attitude, Time.deltaTime * rotationHandler);

        textToSet.text = Input.gyro.attitude.ToString();
    }

    void RotateOnAccelerometer()
    {
        //use acceleration, then remap from (-1, 1) to limit axis
        Vector3 inputPosition = Input.acceleration;
        inputPosition.x = Remap(inputPosition.x, -1, 1, -xAxis, xAxis);
        inputPosition.y = Remap(inputPosition.y, -1, 1, -zAxis, zAxis);

        //set euler angles
        Vector3 euler = new Vector3(inputPosition.y, 0, -inputPosition.x);
        objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, Quaternion.Euler(euler), Time.deltaTime * rotationHandler);

        textToSet.text = euler.ToString();

        //objectToRotate.eulerAngles += new Vector3(-Input.acceleration.y, 0, Input.acceleration.x) * rotationHandler * Time.deltaTime;
    }

    #endregion

    #region public API

    public void SetUseGyro(bool use)
    {
        useGyro = use;
    }

    #endregion

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
