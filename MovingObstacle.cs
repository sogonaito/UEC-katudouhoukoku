using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.right;
    [SerializeField] private float distance = 3f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float movement =
            Mathf.PingPong(Time.time * speed, distance);

        transform.position =
            startPosition + direction.normalized * movement;
    }
}
