using UnityEngine;

public class HiddenData : MonoBehaviour
{
    public int visibleValue = 1;
    int hiddenValue = 0;

    void Update()
    {
        hiddenValue++;
        visibleValue = hiddenValue;
    }
}
