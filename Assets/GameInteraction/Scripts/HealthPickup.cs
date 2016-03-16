using UnityEngine;

namespace GameInteraction
{
    public class HealthPickup : Pickup
    {
        protected override void OnInteract(Ball ball)
        {
            ball.AddHealth(value);
        }
    }
}
