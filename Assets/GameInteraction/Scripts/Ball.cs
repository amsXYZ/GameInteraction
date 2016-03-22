using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameInteraction
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private int _health = 10;
        public int health { get { return _health; } }

        [SerializeField]
        private int fallDamageVelocity = 3;

        [SerializeField]
        private float movePower = 5; // The force added to the ball to move it.

        [SerializeField]
        private float airMovePower = 3; // The force added to the ball to move it.

        [SerializeField]
        private float maxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

        [SerializeField]
        private float jumpPower = 1; // The force added to the ball when it jumps.

        [SerializeField]
        private float maxSpeed = 10f;

        [SerializeField]
        private float minWalkableNormal = 0.75f;

        // If the player is touching any normal >= minWalkableNormal.
        private bool isGrounded = false;

        private const float groundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private float fallVelocity = 0f;
        new public Rigidbody rigidbody { get; private set; }

        private Material ballMaterial;
        
        public int score { get; private set; }

        private void Awake()
        {
            Application.targetFrameRate = 30;
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.maxAngularVelocity = maxAngularVelocity;

            ballMaterial = GetComponent<MeshRenderer>().material;
        }

        public void Move(Vector3 moveDirection, bool jump)
        {
            // Player is touching the floor.
            if (isGrounded)
            {
                // Rotate the ball when the player wants to move.
                rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * movePower);

                if (jump)
                    rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);                
            }
            else // Air movement.
            {
                // Still add torque when in the air for visual effect.
                rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * airMovePower);

                // Add force to the ball if we are moving in the air.
                rigidbody.AddForce(moveDirection * airMovePower);
                fallVelocity = rigidbody.velocity.y;
            }

            // Clamp velocity to max speed.
            rigidbody.velocity = rigidbody.velocity.magnitude > maxSpeed ? rigidbody.velocity.normalized * maxSpeed : rigidbody.velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            ApplyFallDamage(collision);
            isGrounded = CheckGrounded(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            isGrounded = CheckGrounded(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            isGrounded = false;
        }

        private bool CheckGrounded(Collision collision)
        {
            foreach (ContactPoint contactPoint in collision.contacts)
            {
                if (contactPoint.normal.y >= minWalkableNormal)
                    return true;
            }
            return false;
        }

        private void ApplyFallDamage(Collision collision)
        {
            // Don't apply fall damage if we hit a wall or ceiling.
            if (collision.contacts[0].normal.y < minWalkableNormal) return;

            bool bouncy = false;
            if (collision.collider && collision.collider.material)
                bouncy = collision.collider.material.bounciness == 1;

            if (!bouncy) // Don't take fall damage into a count when we hit a bouncy surface.
            {
                int fallDamage = Mathf.Max(Mathf.RoundToInt(Mathf.Abs(fallVelocity) - fallDamageVelocity), 0);
                fallVelocity = 0f; // Reset fall velocity to zero as soon as we hit something.
                if (fallDamage > 0)
                    StartCoroutine(TakeDamage(fallDamage));
            }
        }

        public IEnumerator TakeDamage(int value)
        {
            _health -= value;
            _health = Mathf.Max(_health, 0);
            Debug.Log("Player took " + value + " damage!");
            if (health <= 0)
                OnDeath();
            ballMaterial.SetFloat("_Hurt", 1.0f);
            Camera.main.GetComponent<SmoothFollow>().screenShake = true;
            yield return new WaitForSeconds(.1f);
            ballMaterial.SetFloat("_Hurt", 0.0f);
            yield return new WaitForSeconds(.1f);
            Camera.main.GetComponent<SmoothFollow>().screenShake = false;
        }

        public void AddHealth(int value)
        {
            _health += value;
            Debug.Log( value + " health added, new health: " + _health + ".");
        }

        public void AddScore(int value)
        {
            score += value;
            Debug.Log(value + " score added, new score: " + score + ".");
        }

        protected virtual void OnDeath()
        {
            Debug.Log("Player is dead :(.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
