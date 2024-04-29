using UnityEngine;

public class CustomWarning : MonoBehaviour
{
    void Start()
    {
        Debug.LogWarning("This is a custom warning message.", gameObject);
    }
}
