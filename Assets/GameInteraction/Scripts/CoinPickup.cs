using UnityEngine;

namespace GameInteraction
{
    public class CoinPickup : Pickup
    {
        protected override void OnInteract(Ball ball)
        {
            ball.AddScore(value);
        }
    }
}
