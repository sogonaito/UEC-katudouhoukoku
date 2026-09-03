using System.Collections;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 3f;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Animator animatorComponent;

    private Transform player;
    private PlayerController playerController;
    private CharacterController characterController;
    private FallRagdoll fallRagdoll;

    private Transform[] poseTransforms;
    private Vector3[] startLocalPositions;
    private Quaternion[] startLocalRotations;

    private bool activated;

    private void Awake()
    {
        player = transform.parent;

        ragdollBodies = GetComponentsInChildren<Rigidbody>(true);
        ragdollColliders = GetComponentsInChildren<Collider>(true);
        animatorComponent = GetComponentInChildren<Animator>();

        poseTransforms = GetComponentsInChildren<Transform>(true);
        startLocalPositions = new Vector3[poseTransforms.Length];
        startLocalRotations = new Quaternion[poseTransforms.Length];

        for (int i = 0; i < poseTransforms.Length; i++)
        {
            startLocalPositions[i] = poseTransforms[i].localPosition;
            startLocalRotations[i] = poseTransforms[i].localRotation;
        }

        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            characterController = player.GetComponent<CharacterController>();
            fallRagdoll = player.GetComponent<FallRagdoll>();
        }

        SetRagdoll(false);
    }

    private void SetRagdoll(bool enabled)
    {
        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = !enabled;
            body.useGravity = enabled;

            if (!enabled)
            {
                body.linearVelocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }
        }

        foreach (Collider hitBox in ragdollColliders)
        {
            hitBox.enabled = enabled;
        }
    }

    public void Activate()
    {
        if (activated)
        {
            return;
        }

        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point が設定されていません。");
            return;
        }

        activated = true;

        if (animatorComponent != null)
        {
            animatorComponent.enabled = false;
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        transform.SetParent(null, true);
        SetRagdoll(true);

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        SetRagdoll(false);

        player.position = respawnPoint.position;
        player.rotation = respawnPoint.rotation;

        transform.SetParent(player, false);

        for (int i = 0; i < poseTransforms.Length; i++)
        {
            poseTransforms[i].localPosition = startLocalPositions[i];
            poseTransforms[i].localRotation = startLocalRotations[i];
        }

        if (animatorComponent != null)
        {
            animatorComponent.enabled = true;
        }

        if (characterController != null)
        {
            characterController.enabled = true;
        }

        if (playerController != null)
        {
            playerController.enabled = true;
        }

        if (fallRagdoll != null)
        {
            fallRagdoll.ResetFall();
        }

        activated = false;
    }
}
