using UnityEngine;

namespace GameInteraction
{
    public class ZeroGravity : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody)
                rigidbody.useGravity = false;
        }

        private void OnTriggerExit(Collider collider)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody)
                rigidbody.useGravity = true;
        }
    }
}
