using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Controls
    public float smoothFollow = 8;
    public Transform topLeftThreshold;
    public Transform bottomRightThreshold;

    // Components
    private Vector3 target;
    private Transform player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update ()
    {
        // Get a reference to the players position
        target = new Vector3(player.position.x, transform.position.y, player.position.z);

        // Check if players z position is within the left and right threshold
        if (target.z < topLeftThreshold.position.z && target.z > bottomRightThreshold.position.z) {
            // Follow the players z position
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.y, target.z), Time.deltaTime * smoothFollow);
        }

        // Check if players x position is within the top and bottom threshold
        if(target.x < bottomRightThreshold.position.x && target.x > topLeftThreshold.position.x) {
            // Follow the players x position
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.x, target.y, transform.position.z), Time.deltaTime * smoothFollow);
        }

    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(topLeftThreshold.position, new Vector3(bottomRightThreshold.position.x, 0, topLeftThreshold.position.z));
        Gizmos.DrawLine(topLeftThreshold.position, new Vector3(topLeftThreshold.position.x, 0, bottomRightThreshold.position.z));

        Gizmos.DrawLine(bottomRightThreshold.position, new Vector3(topLeftThreshold.position.x, 0, bottomRightThreshold.position.z));
        Gizmos.DrawLine(bottomRightThreshold.position, new Vector3(bottomRightThreshold.position.x, 0, topLeftThreshold.position.z));
    }
}
