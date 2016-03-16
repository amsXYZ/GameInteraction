using UnityEngine;

namespace GameInteraction
{
    public class DeathTrigger : Interactable
    {
        protected override void OnInteract(Ball ball)
        {
            ball.TakeDamage(ball.health);
        }
    }
}
