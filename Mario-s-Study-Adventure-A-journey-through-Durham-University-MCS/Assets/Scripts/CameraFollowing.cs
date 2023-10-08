using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make sure the camera is following the player
// code base on: https://github.com/zigurous/unity-super-mario-tutorial
public class CameraFollowing : MonoBehaviour
{
    private Transform player;

    // ground hight
    public float height = 2.1f;

    // underground hight
    public float undergroundHeight = -12.6f;

    // identify the player position
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // make camera position is player position
    private void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = player.position.x + 4f;
        transform.position = cameraPos;
    }

    // if underground position, then set the height to the underground one
    public void UnderGround(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
