using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        Vector3 velocity = characterController.velocity;
        velocity.y = 0f;

        float speed = velocity.magnitude;

        characterAnimator.SetFloat("Speed", speed);
        characterAnimator.SetBool("Grounded", characterController.isGrounded);
    }
}
