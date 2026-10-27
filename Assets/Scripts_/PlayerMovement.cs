
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float swipeSensitivity = 0.01f;

    private Rigidbody rb;
    private Vector2 swipeDelta;
    private Vector2 lastTouchPosition;
    private bool isTouching;

    private float horizontal;
    private float vertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Kontrol keyboard untuk komputer
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Kontrol usap layar untuk HP
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 delta = touch.position - lastTouchPosition;

                horizontal = Mathf.Clamp(
                    delta.x * swipeSensitivity, -1f, 1f);

                vertical = Mathf.Clamp(
                    delta.y * swipeSensitivity, -1f, 1f);

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended ||
                     touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
                horizontal = 0f;
                vertical = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 direction =
            new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 nextPosition = rb.position
            + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }
}
