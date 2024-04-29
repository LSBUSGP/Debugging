using UnityEngine;

public class CustomError : MonoBehaviour
{
    void Start()
    {
        Debug.LogError("This is a custom error message.", gameObject);
    }
}
