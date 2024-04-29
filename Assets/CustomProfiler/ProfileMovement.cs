using Unity.Profiling;
using UnityEngine;

public class ProfileMovement : MonoBehaviour
{
    public float speed = 5f;
    public float smoothTime = 1f;
    float velocity = 0f;

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        Vector3 position = transform.position;
        float input = Input.GetAxis("Horizontal");
        float target = position.x + input * speed;
        position.x = Mathf.SmoothDamp(position.x, target, ref velocity, 1f);
        transform.position = position;
    }
}
