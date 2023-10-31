using UnityEngine;

public class DJDIscSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;

    private void Update()
    {
        transform.Rotate(0f, spinSpeed, 0f);
    }
}
