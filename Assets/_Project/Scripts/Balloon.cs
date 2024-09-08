using UnityEngine;

namespace _Project.Scripts
{
    public enum Type
    {
        Red,
        Blue,
    }

    public class Balloon : MonoBehaviour, IPoppable
    {
        public Type type;
        [SerializeField] private float buoyancyForce;
        [SerializeField] private float gravityForce;
        [SerializeField] private float upwardLimit = 1.0f;
        [SerializeField] private ParticleSystem popParticle;
        [SerializeField] private float blastRadius = 5.0f; // Radius of the explosion effect
        [SerializeField] private float blastForce = 500f;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            // Apply buoyancy force when the balloon is below the upward limit
            if (transform.position.y < upwardLimit)
            {
                rb.AddForce(Vector3.up * buoyancyForce * Random.Range(-0.3f, 0.3f), ForceMode.Force);
            }
            else
            
            {
                // Apply a small downward force to stabilize the height
                rb.AddForce(Vector3.down * 0.5f * Random.Range(-0.3f, 0.3f), ForceMode.Force);
            }

            // Apply a custom gravity force downward to simulate the balloon's natural behavior
            rb.AddForce(Vector3.down * gravityForce * Random.Range(-0.4f, 0.4f), ForceMode.Force);
        }

        private void ApplyBlastEffect()
        {
            // Get all nearby colliders within the blast radius
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            // Apply explosion force to each balloon in the blast radius
            foreach (Collider nearbyCollider in colliders)
            {
                Rigidbody nearbyRb = nearbyCollider.GetComponent<Rigidbody>();

                if (nearbyRb != null && nearbyRb != rb) // Ensure it's not the current balloon
                {
                    nearbyRb.AddExplosionForce(blastForce, transform.position, blastRadius);
                }
            }
        }


        public void Pop(Type playerColor)
        {
            if (playerColor != type) return;
            Debug.Log($"{gameObject.name} popped");
            ApplyBlastEffect();
            GameManager.Instance.RemoveBalloonFromList(this);
            popParticle.Play();
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}