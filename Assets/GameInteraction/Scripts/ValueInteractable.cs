using UnityEngine;

namespace GameInteraction
{
    public abstract class ValueInteractable : Interactable
    {
        [SerializeField]
        private int _value = 1;
        public int value { get { return _value; } protected set { _value = value; } }
    }
}