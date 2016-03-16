using UnityEngine;

namespace GameInteraction
{
    public class Trampoline : ValueInteractable
    {
        protected override void OnInteract(Ball ball)
        {
            float ballSpeed = ball.rigidbody.velocity.magnitude;
            Vector3 bounceDirection =GetComponent<Transform>().up; // Don;t need to reflect it since a bounce physics material will do that for us.
            Vector3 impulse = bounceDirection * ballSpeed * value * 0.01f;
            ball.rigidbody.AddForce(impulse, ForceMode.Impulse);
        }
    }
}
