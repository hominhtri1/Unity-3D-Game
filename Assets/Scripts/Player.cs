using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public AudioClip jumpAudio;

    private Rigidbody body;

    private AudioSource audioSource;

    void Start()
    {
        body = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
            Application.Quit();

        if (transform.position.y < -10)
            Retry();
        
        scoreText.text = ((int) transform.position.z).ToString();
    }

    void FixedUpdate()
    {
        float sidewaysForce = 20;
        float maxSidewaysVelocity = 5;

        float forwardForce = 30;
        float maxForwardVelocity = 8;

        float verticalForce = 100;

        float forceX = 0;

		if (Input.GetKey("left"))
			forceX = -sidewaysForce;
		
		if (Input.GetKey("right"))
			forceX = sidewaysForce;

        if (Math.Abs(body.velocity.x) > maxSidewaysVelocity)
			forceX = 0;
        
        float forceZ = 0;

        /*
        if (Input.GetKey("up"))
			forceZ = horizontalForce;
		
		if (Input.GetKey("down"))
			forceZ = -horizontalForce;
        */

        forceZ = forwardForce;

        if (Math.Abs(body.velocity.z) > maxForwardVelocity)
			forceZ = 0;
        
        float forceY = 0;

        if (Input.GetKey("space") && IsGrounded())
        {
            forceY = verticalForce;

            audioSource.PlayOneShot(jumpAudio);
        }

        body.AddForce(forceX, forceY, forceZ);
    }

    void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
    }

    bool IsGrounded()
	{
		LayerMask mask = ~(1 << 8);

		return Physics.Raycast(transform.position, Vector3.down, 1.5f, mask);
	}
}
