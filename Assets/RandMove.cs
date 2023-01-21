using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandMove : MonoBehaviour
{
    private float speed = 5;
    private int repeatSpeed = 0;
    private int direction = 3;
    private int rotateTime = 0;
    public bool detected;
    private GameObject platform;


    private Transform platformTransform;

    private float xEdge;
    private float zEdge;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = GameObject.FindGameObjectWithTag("Platform");
        platformTransform = platform.GetComponent<Transform>();
        // xEdge = platformTransform.position.x;
        // zEdge = platformTransform.position.z;
    }   

    // Update is called once per frame
    void Update()
    {
        System.Random rd = new System.Random();
        
        if (repeatSpeed < 2000){
            repeatSpeed += rd.Next(3);
        } else {
            repeatSpeed = 0;
            direction = rd.Next(1, 5);
            rotateTime = 0;
        } 


        move(direction);
    }

    public void move(int direction) {
        if (-90f <= transform.position.x 
        && transform.position.x <= 90f 
        && -90f <= transform.position.z
        && transform.position.z <= 90f)
        {
            if (rotateTime < 180){
            if(direction == 1) {
                transform.Rotate(new Vector3(0, .5f, 0), Space.Self);
            }
            if(direction == 2){
                 transform.Rotate(new Vector3(0, -.5f, 0), Space.Self);
            }
            if(direction == 4){
                 transform.Rotate(Vector3.up, 1, Space.Self);
            }
            rotateTime ++;
            } else {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        } 
        else 
        {
            transform.Rotate(new Vector3(0, .5f, 0), Space.Self);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player")
        {
            detected = true;
        }
    }

}
