using Photon.Pun;
using UnityEngine;

namespace _Project.Scripts
{
    public class Dart : MonoBehaviour
    {
        [SerializeField] private Type playerType;
        public bool isFlying;
        [SerializeField] private Rigidbody dartRb;


        void Update()
        {
            dartRb.constraints = isFlying ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.None;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (GameManager.Instance.isGrabbing) return;
            // Check if the other object implements the IPoppable interface
            IPoppable poppable = other.gameObject.GetComponent<IPoppable>();
            if (poppable != null)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("PopBalloon", RpcTarget.All);
                // Call the Pop method with the corresponding playerColor (you can decide how to determine this)
                poppable.Pop(playerType); // Assume playerType is assigned elsewhere in your logic
                Debug.Log("Dart hit poppable object");
            }
        }

        public void SetFlyingToTrue()
        {
            isFlying = true;
        }

        public void SetFlyingToFalse()
        {
            isFlying = false;
        }
    }
}