using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomFunctions;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // Controls
    public float walkSpeed = 5;
    public float sprintSpeed = 8;
    public float rotationSpeed = 800;

    private Quaternion targetRotation;

    // Components
    public Gun gun;
    private CharacterController controller;
    private Camera cam;

    void Start ()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update ()
    {
        Movement();
        Rotation();

        if (Input.GetButtonDown("Fire")) { // If the fire button is clicked
            gun.Shoot();
        } else if (Input.GetButton("Fire")) { // If the fire button is held down
            gun.ShootAuto();
        }
    }

    void Movement ()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 movement = input.normalized; // Normalized so that diagonal movement is not faster
        movement *= Input.GetButton("Sprint") ? sprintSpeed : walkSpeed; // Apply speed dependant on sprint button input
        movement += Vector3.up * -9.81f; // Gravity applied to the player
        controller.Move(movement * Time.deltaTime);
    }

    void Rotation ()
    {
        Vector3 mp = Input.mousePosition;
        mp = cam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mp - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Maths.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
    }
}
