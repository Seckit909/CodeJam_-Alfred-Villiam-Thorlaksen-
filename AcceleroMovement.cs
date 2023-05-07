using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleroMovement : MonoBehaviour
{
    [SerializeField] private float Movespeed = 0.5f;
    [SerializeField] private float maxMagnitude = 1.0f;
    private Camera mainCamera;
    private float cameraWidth;
    private float cameraHeight;
    public float camerasize = 40f;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = camerasize * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.acceleration.x, Input.acceleration.y);
        float acceleration = Movespeed;

        // Restrict movement to 2D space
        direction = Vector2.ClampMagnitude(direction, maxMagnitude);

        //Found some helpfull unity implementation https://docs.unity3d.com/ScriptReference/Vector2.html and took inspiration from https://www.youtube.com/watch?v=L6Q0h8VNbGk&ab_channel=CollegeCode with moving gameobjects

        // here we Calculate the new position after applying the movement of the object
        Vector3 newPosition = transform.position + (Vector3)(direction * acceleration);

        // Here we are calculating the min and max values within the Main camera 
        float xMin = mainCamera.transform.position.x - cameraWidth;
        float xMax = mainCamera.transform.position.x + cameraWidth;
        float yMin = mainCamera.transform.position.y - cameraHeight;
        float yMax = mainCamera.transform.position.y + cameraHeight;

        // this Clamps or restricts the position within the camera bounds
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax); //Took some inspiration from https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html 


        // Update the position of the object
        transform.position = newPosition;
    }
}
