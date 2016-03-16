using UnityEngine;

namespace GameInteraction
{
    public abstract class Interactable : MonoBehaviour
    {
        // If we are a collider.
        protected virtual void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.collider.GetComponent<Ball>();
            if (ball)
                OnInteract(ball);
        }

        // If we are a collider.
        protected virtual void OnCollisionStay(Collision collision)
        {
            Ball ball = collision.collider.GetComponent<Ball>();
            if (ball)
                OnInteract(ball);
        }

        // If we are a trigger.
        protected virtual void OnTriggerEnter(Collider collider)
        {
            Ball ball = collider.GetComponent<Ball>();
            if (ball)
                OnInteract(ball);
        }

        protected abstract void OnInteract(Ball ball);
    }
}