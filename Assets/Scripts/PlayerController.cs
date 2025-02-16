using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    private Animator animator;
    [SerializeField] private float speed = 2f;
    [SerializeField] float rotationSpeed = 10f;


    void Start(){
        animator = GetComponent<Animator>();

    }

    void OnMove(InputValue value) {
        movementInput = value.Get<Vector2>() * Time.deltaTime * speed;

        print("movementInput: " + movementInput);
    }

    void Update() {
        //Apply the movement input to character
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y).normalized;

           // Check if the character is moving
        bool isWalking = movement != Vector3.zero;

        // Update the Animator parameter
        animator.SetBool("IsWalking", isWalking);
        print("isWalking: " + isWalking);
          // Move the character
        if (isWalking)
        {
            MoveCharacter(movement);
            RotateCharacter(movement);
        }
    }

    private void MoveCharacter(Vector3 direction)
    {
        // Move the character in the direction of input
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void RotateCharacter(Vector3 direction)
    {
        // Calculate the target rotation to face the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate the character towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
