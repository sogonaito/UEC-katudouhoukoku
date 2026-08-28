using UnityEngine;

public class FallRagdoll : MonoBehaviour
{
    [SerializeField] private RagdollController ragdoll;
    [SerializeField] private float ragdollStartY = -4f;

    private bool hasFallen;

    private void Update()
    {
        if (!hasFallen && transform.position.y < ragdollStartY)
        {
            hasFallen = true;
            ragdoll.Activate();
        }
    }

    public void ResetFall()
    {
        hasFallen = false;
    }
}
