using TMPro;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalText;

    private bool goalReached;

    private void Start()
    {
        if (goalText != null)
        {
            goalText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (goalReached)
        {
            return;
        }

        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        goalReached = true;

        if (goalText != null)
        {
            goalText.gameObject.SetActive(true);
        }

        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
