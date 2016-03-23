using UnityEngine;

namespace GameInteraction
{
    public class BoostRamp : ValueInteractable
    {
        protected override void OnInteract(Ball ball)
        {
            ball.rigidbody.AddForce(transform.forward * value);
        }
    }
}
