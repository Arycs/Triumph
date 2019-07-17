using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GameObject cameraHandler;
    private Vector3 cameraDampVelocity;
    public float cameraDampValue = 0.1f;

    void Start () {
    }
	
	void FixedUpdate () {

        transform.position = Vector3.SmoothDamp(transform.position, cameraHandler.transform.position, ref cameraDampVelocity, cameraDampValue);
    }
}
