using UnityEngine;

namespace GameInteraction
{
    public class CoinDoorTrigger : ValueInteractable
    {
        [SerializeField]
        new private Animation animation;

        private bool _isOpen = false;
        public bool isOpen{ get { return _isOpen; } protected set { _isOpen = value; } }

        protected override void OnInteract(Ball ball)
        {
            if (ball.score >= value && !isOpen && animation)
            {
                animation.Play();
                isOpen = true;
            }
        }
    }
}
