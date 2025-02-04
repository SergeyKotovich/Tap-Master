using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Singularity : MonoBehaviour
{
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;

    private void Awake()
    {
        m_GravityRadius = GetComponent<SphereCollider>().radius;

        if (GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody && other.GetComponent<SingularityPullable>())
        {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity *
                                             other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
        }
    }
}