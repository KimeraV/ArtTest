using UnityEngine;
using UnityEngine.VFX;
[ExecuteInEditMode]
public class ShaderPosition : MonoBehaviour
{
    [Header("TorchSettings")]
    [SerializeField] float lightRadius = 1f;
    [SerializeField] float smokeRadius = 1f;
    [Header("Dependencies")]
    [SerializeField] VisualEffect smokeVFX;
    private readonly int positionID = Shader.PropertyToID("_Position");
    private readonly int radiusID = Shader.PropertyToID("_Radius");
    private readonly int smokeRadiusID = Shader.PropertyToID("SmokeRadius");

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lightRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, smokeRadius);
    }
    void Update()
    {
        Shader.SetGlobalVector(positionID, transform.position);
        Shader.SetGlobalFloat(radiusID, lightRadius);
        smokeVFX.SetFloat(smokeRadiusID, smokeRadius);
    }

    public void SetRadius(float radius)
    {
        lightRadius = radius;
        smokeRadius = radius;
    }
}
