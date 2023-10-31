using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.01f;
    [SerializeField] private float maxSpeed = 0.1f;
    
    public new Light light;

    private void Awake()
    {
        light = GetComponent<Light>();
    }

    private void Start()
    {
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));

            light.enabled = !light.enabled;

        }
    }
}
