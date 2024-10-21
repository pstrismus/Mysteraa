//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Camera_move : MonoBehaviour
//{
//    [SerializeField] [Range(50, 500)] int senstive = 100;
//    [SerializeField] float xRot;
//    [SerializeField] float yRot;

//    [SerializeField] Transform body;
//    private void Awake()
//    {

//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//    }


//    private void Update()
//    {
//        MouseMove();
//    }
//    private void MouseMove()
//    {
//        yRot = 0;

//        xRot += Input.GetAxisRaw("Mouse Y")* senstive * Time.deltaTime;     
//        yRot += Input.GetAxisRaw("Mouse X")* senstive * Time.deltaTime;     
//        xRot = Mathf.Clamp(xRot, -50f, 50f);
//        transform.localRotation = Quaternion.Euler(-xRot, 0, 0);
//        body.Rotate(0, yRot, 0);


//    }
//}
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    [SerializeField] [Range(50, 500)] int sensitivity = 100;
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;

    [SerializeField] Transform target;
    [SerializeField] float distanceFromTarget = 3f;
    [SerializeField] Vector2 pitchLimits = new Vector2(-30f, 60f);
    [SerializeField] float tampon = 0.2f;
    [SerializeField] LayerMask collisionLayers;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseMove();
        AdjustDistance();
    }

    private void MouseMove()
    {
        xRotation -= Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        yRotation += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, pitchLimits.x, pitchLimits.y);

        Vector3 direction = new Vector3(0, 0, -distanceFromTarget);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 desiredCameraPos = target.position + rotation * direction;

        if (Physics.Linecast(target.position, desiredCameraPos, out RaycastHit hit, collisionLayers))
        {
            desiredCameraPos = hit.point - tampon * hit.normal;
        }

        transform.position = desiredCameraPos;
        transform.LookAt(target);
    }

    private void AdjustDistance()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            distanceFromTarget -= scrollInput;
            distanceFromTarget = Mathf.Clamp(distanceFromTarget, 2f, 5.5f);
        }
    }
}

