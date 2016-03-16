using System;
using UnityEngine;

namespace GameInteraction
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Pickup : ValueInteractable
    {
        protected override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);
            Destroy(gameObject);
        }
    }
}
