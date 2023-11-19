using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotSpawn : MonoBehaviour
{
    public GameObject basketBall;
    public GameObject goal;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            velCalc();
        }
    }

    // Velocity Calculator
    void velCalc()
    {
        // Create Basketball shape
        GameObject Ball = Instantiate(basketBall, transform.position, Quaternion.identity);

        // Assigning A Variable To The Rigidbody
        Rigidbody rb = Ball.GetComponent<Rigidbody>();
        
        // Basketball Positions
        double ballX = rb.transform.position.x;
        double ballY = rb.transform.position.y;
        double ballZ = rb.transform.position.z;

        // Goal Positions
        double goalX = 5 - goal.transform.position.x;
        double goalY = goal.transform.position.y - .05;
        double goalZ = goal.transform.position.z;

        // Distances
        double disX = goalX - ballX;
        double disY = goalY - ballY;
        double disZ = goalZ - ballZ;

        // Delta
        double delta = ((Math.PI / 2) + Math.Atan(disY / disY)) / 2 * (180 / Math.PI);

        // Velocity Calculator
        double vel = Math.Sqrt(Math.Pow(disX, 2) * 9.81 / ((disX * Math.Sin(2 * delta)) - (2 * disY * Math.Pow(Math.Cos(delta), 2))));
        double velX = vel * Math.Cos(delta);
        double velY = vel * Math.Sin(delta);

        // Time Calculator
        double time = (-velY + Math.Sqrt(Math.Pow(velY, 2) + 2 * 9.81 * disY)) / 9.81;

        // Z Velocity
        double velZ = disZ / time;

        // Giving The Basketball A Velocity
        rb.AddForce(-transform.right * (float)velX, ForceMode.VelocityChange);
        rb.AddForce(-transform.up * (float)velY, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * (float)velZ, ForceMode.VelocityChange);
    }
}