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
            if (Camera.main)
                Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Bloom>().enabled = true;
        }

        private void OnTriggerExit(Collider collider)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody)
                rigidbody.useGravity = true;
            if (Camera.main)
                Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Bloom>().enabled = false;
        }
    }
}
