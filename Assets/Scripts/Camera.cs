using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Camera : MonoBehaviour
{
    public Transform player;

    public GameObject ground;

    private bool generatedGround;

    private System.Random random;

    void Start()
    {
        generatedGround = false;

        random = new System.Random();
    }

    void Update()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = player.position;

        transform.position =
            new Vector3(playerPosition.x, cameraPosition.y, playerPosition.z - 10);

        int iteration = (int) transform.position.z / 40 + 2;
        float relativeZ = transform.position.z % 40;
        
        if (relativeZ > 1 && !generatedGround)
        {
            float nextX = (float) (random.NextDouble() * 2 - 1) * 7;
            float nextY = 0;
            float nextZ = iteration * 40;

            Instantiate(ground, new Vector3(nextX, nextY, nextZ), Quaternion.identity);

            generatedGround = true;
        }

        if (relativeZ < 1)
            generatedGround = false;
    }
}
