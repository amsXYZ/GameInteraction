using UnityEngine;
using System.Collections;
using GameInteraction;

[RequireComponent(typeof(Ball), typeof(MeshRenderer))]
public class HealthFlash : MonoBehaviour {

    Ball ball;
    Material ballMaterial;
    int previousHealth;

	// Use this for initialization
	void Start () {

        ball = GetComponent<Ball>();
        ballMaterial = GetComponent<MeshRenderer>().material;
        previousHealth = ball.health;

	}
	
	// Update is called once per frame
	void Update () {

        if(ball.health < previousHealth)
        {
            previousHealth = ball.health;
            StartCoroutine(FlashRed());
        }
	
	}

    public IEnumerator FlashRed()
    {
        ballMaterial.SetFloat("_Hurt", 1.0f);
        //Camera.main.GetComponent<SmoothFollow>().screenShake = true;
        yield return new WaitForSeconds(.1f);
        ballMaterial.SetFloat("_Hurt", 0.0f);
        yield return new WaitForSeconds(.1f);
        //Camera.main.GetComponent<StudentSmoothFollow>().screenShake = false;
    }
}
