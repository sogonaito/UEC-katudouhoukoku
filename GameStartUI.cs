using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CameraFollow cameraFollow;

    private void Start()
    {
        playerController.enabled = false;
        cameraFollow.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);

        playerController.enabled = true;
        cameraFollow.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
