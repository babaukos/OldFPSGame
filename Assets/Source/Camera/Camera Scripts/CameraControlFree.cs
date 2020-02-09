//
// Модуль управления свободной камерой
//

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera/Controller/Camera Free")]
public class CameraControlFree : MonoBehaviour 
{

    [Tooltip("Скорость скольжения камеры ")]
    public float moveSpeed = 9;         // скорость передвижения камеры 
    [Tooltip("Скорость передвижения камеры ")]
    public float moveAxelerate = 10;    // максимальная скорость передвижения камеры 
    public float qurentSpeed;

    [Tooltip("Скорость вращания кмеры")]
    public float rotateSpeed = 57;       // скорость поворота камеры 
    public Limit limitRotatationX;
    public Limit limitRotatationY;

    [Tooltip("Текущий зум")]
    public float qurentZoom = 60;
    [Tooltip("Скорость зумирования (шанг)")]
    public float stepZoom = 10;         // скорость приблежения камеры
    public Limit limitZoom;

    public bool debagInformation = true;

    private Camera camera;
    private float rotY, rotX;
    private float movX, movY;


    [System.Serializable]
    public struct Limit 
    {
        public float minimum;
        public float maximum;

        public void SetValue(float min, float max) 
        {
            minimum = min;
            maximum = max;
        }
    }

    private void Start() 
    {
        camera = GetComponent<Camera>();
        qurentSpeed = moveSpeed;
    }

	private void Update()
	{
	   InputIn();
	}

    private void InputIn()
    {
        // Вращение
        if (Input.GetMouseButton(1))
        {                                     
            rotY += Input.GetAxis("Mouse X") * rotateSpeed;
            rotY = ClampAngle(rotY, limitRotatationY.minimum, limitRotatationY.maximum);

            rotX -= Input.GetAxis("Mouse Y") * rotateSpeed;
            rotX = ClampAngle(rotX, limitRotatationX.minimum, limitRotatationX.maximum);
        }

        // Ускарение кмеры
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            qurentSpeed = moveSpeed * moveAxelerate;
        }
        else
            {
                qurentSpeed = moveSpeed;
            }

        // зумирование
        qurentZoom += -Input.GetAxis("Mouse ScrollWheel") * stepZoom;
        qurentZoom = Mathf.Clamp(qurentZoom, limitZoom.minimum, limitZoom.maximum);
        
        // перемещение
        movY = Input.GetAxis("Horizontal");
        movX = Input.GetAxis("Vertical");

        // 
        var rotation = Quaternion.Euler(rotX, rotY, 0);
        var position = new Vector3(movY, 0, movX) * Time.deltaTime * qurentSpeed;

        camera.fieldOfView = qurentZoom;
        transform.localRotation = rotation;
        transform.position += transform.TransformDirection(position);
    }

    private void OnGUI()
    {
        if (debagInformation)
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(5, 5, 300, 50), "Position: " + "  x: " + transform.position.x.ToString("0.00") + "  y: " + transform.position.y.ToString("0.00") + "  z: " + transform.position.z.ToString("0.00"));
            GUI.Label(new Rect(5, 25, 300, 50),"Rotation: " + "  x: " + rotX.ToString("0.00") + "  y: " + rotY.ToString("0.00"));
            GUI.Label(new Rect(5, 45, 300, 50),"Zoom: " + qurentZoom);
        }
    }

    public float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }

    public float ZoomLimit(float dist, float min, float max)
    {
        if (dist < min)

            dist = min;

        if (dist > max)

            dist = max;

        return dist;
    }
}