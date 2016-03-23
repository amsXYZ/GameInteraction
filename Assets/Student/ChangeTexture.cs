using UnityEngine;
using System.Collections;
using GameInteraction;

[RequireComponent(typeof(Ball), typeof(MeshRenderer))]
public class ChangeTexture : MonoBehaviour {

    public Texture2D[] textures = new Texture2D[4];

    private Ball ball;
    private Material ballMaterial;
    private int previousTexture;

	// Use this for initialization
	void Start () {

        ball = GetComponent<Ball>();
        ballMaterial = GetComponent<MeshRenderer>().material;
        previousTexture = ball.score;

	}
	
	// Update is called once per frame
	void Update () {

        if(ball.score > previousTexture)
        {
            previousTexture = ball.score;
            ballMaterial.SetTexture("_MainTex", textures[ball.score]);
        }
	
	}
}
