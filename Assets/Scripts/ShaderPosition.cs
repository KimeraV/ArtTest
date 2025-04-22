using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class ShaderPosition : MonoBehaviour
{
    [Header("TorchSettings")]
    [SerializeField] float lightRadius = 1f;
    [SerializeField] float smokeRadius = 1f;
    [SerializeField] bool syncRadius = true;
    [Header("Dependencies")]
    [SerializeField] VisualEffect smokeVFX;
    private readonly int positionID = Shader.PropertyToID("_Position");
    private readonly int radiusID = Shader.PropertyToID("_Radius");
    private readonly int smokeRadiusID = Shader.PropertyToID("SmokeRadius");

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lightRadius);
        Gizmos.color = Color.blue;
        DrawHorizontalCircleGizmo(transform.position, smokeRadius);
    }

    void DrawHorizontalCircleGizmo(Vector3 center, float radius, int segments = 32)
    {
        segments = Mathf.Max(3, segments);
        Vector3 prevPoint = center + new Vector3(radius, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = (float)i / (float)segments * 2 * Mathf.PI;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
#endif

    void Update()
    {
        if (syncRadius)
        {
            smokeRadius = Mathf.Max(0, lightRadius - 1);
        }

        Shader.SetGlobalVector(positionID, transform.position);
        Shader.SetGlobalFloat(radiusID, lightRadius);
        smokeVFX.SetFloat(smokeRadiusID, smokeRadius);
    }

    public void SetRadius(float radius)
    {
        lightRadius = radius;
        if (syncRadius)
        {
            smokeRadius = Mathf.Max(0, radius - 1);
        }
    }
}