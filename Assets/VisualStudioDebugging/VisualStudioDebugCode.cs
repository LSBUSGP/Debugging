using UnityEngine;

public class VisualStudioDebugCode : MonoBehaviour
{
    // from https://issuetracker.unity3d.com/issues/smoothdamp-behaves-differently-between-positive-and-negative-velocity
    static float SmoothDampUnity(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        // Based on Game Programming Gems 4 Chapter 1.10
        smoothTime = Mathf.Max(0.0001F, smoothTime);
        float omega = 2F / smoothTime;

        float x = omega * deltaTime;
        float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
        float change = current - target;
        float originalTo = target;

        // Clamp maximum speed
        float maxChange = maxSpeed * smoothTime;
        change = Mathf.Clamp(change, -maxChange, maxChange);
        target = current - change;

        float temp = (currentVelocity + omega * change) * deltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        float output = target + (change + temp) * exp;

        // Prevent overshooting
        if (originalTo - current > 0.0F == output > originalTo)
        {
            output = originalTo;
            currentVelocity = (output - originalTo) / deltaTime;
        }

        return output;
    }

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
        position.x = SmoothDampUnity(position.x, target, ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        transform.position = position;
    }
}
