using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [Range(0f, 1f)]
    [SerializeField] private float minColorRange;
    [Range(0f, 1f)]
    [SerializeField] private float maxColorRange;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        Color color = new Color(Random.Range(minColorRange, maxColorRange),
                                Random.Range(minColorRange, maxColorRange),
                                Random.Range(minColorRange, maxColorRange), 1);

        meshRenderer.material.SetColor("_Color", color);
    }
}
