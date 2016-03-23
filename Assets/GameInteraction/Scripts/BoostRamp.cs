using UnityEngine;

namespace GameInteraction
{
    public class BoostRamp : ValueInteractable
    {
        protected override void OnInteract(Ball ball)
        {
            ball.rigidbody.AddForce(transform.forward * value);
            if (Camera.main) Camera.main.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>().enabled = true;
        }

        void OnCollisionExit(Collision collision)
        {
            if (Camera.main) Camera.main.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>().enabled = false;
        }
    }
}
