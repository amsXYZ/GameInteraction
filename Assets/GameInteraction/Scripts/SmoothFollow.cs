using UnityEngine;

namespace GameInteraction
{
    public class SmoothFollow : MonoBehaviour
	{
		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

        public bool screenShake;

        // Update is called once per frame
        void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            if (!screenShake) { transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z); transform.LookAt(target); }
            else
            {
                float dispX = (Mathf.PerlinNoise(Time.time * 100, Time.time * 100) - 0.5f) * 2 / 10;
                float dispY = (Mathf.PerlinNoise(dispX * 256, dispX * 256) - 0.5f) * 2 / 10;
                float dispZ = (Mathf.PerlinNoise(dispY * 256, dispY * 256) - 0.5f) * 2 / 10;
                transform.position = new Vector3(transform.position.x, currentHeight + dispY, transform.position.z);
            }
		}
	}
}