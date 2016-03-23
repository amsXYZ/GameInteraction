using UnityEngine;
using System.Collections;
using GameInteraction;

public class NavigateWaypoints : MonoBehaviour {

    public Transform[] waypoints;
    int currentWaypoint = 0;
    Vector3 speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(currentWaypoint != waypoints.Length)
        {
            transform.position = Vector3.SmoothDamp(transform.position, waypoints[currentWaypoint].position, ref speed, Time.deltaTime, 20.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, waypoints[currentWaypoint].rotation, Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1)
            {
                currentWaypoint++;
            }
        }

        if (currentWaypoint == waypoints.Length)
        {
            if (Quaternion.Angle(transform.rotation, waypoints[currentWaypoint - 1].rotation) == 0)
            {
                this.enabled = false;
                GetComponent<StudentSmoothFollow>().enabled = true;
                FindObjectOfType<BallUserControl>().enabled = true;
            }
            else transform.rotation = Quaternion.Slerp(transform.rotation, waypoints[currentWaypoint - 1].rotation, Time.deltaTime);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Transform p = waypoints[0];
        foreach (Transform t in waypoints)
        {
            Gizmos.DrawLine(p.position, t.position);
            Gizmos.DrawWireSphere(t.position, 1);
            p = t;
        }
    }
}
