using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    public Transform target; //object to follow (e.g. the player)
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 5;

    private Vector2 lookInput;

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            print("FollowCamera: No target assigned!");
            return;
        }

    float horizontalInput = lookInput.x * rotationSpeed * Time.deltaTime;
    float verticalInput = lookInput.y * rotationSpeed * Time.deltaTime;

    offset = Quaternion.Euler(0, horizontalInput, 0) * offset;

    Vector3 verticalRotationAxis = Vector3.Cross(offset, Vector3.up).normalized;
    offset = Quaternion.AngleAxis(verticalInput, verticalRotationAxis) * offset;

    Vector3 desiredPosition = target.position + offset;

    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

    transform.position = smoothedPosition;

    transform.LookAt(target);

    }
}
