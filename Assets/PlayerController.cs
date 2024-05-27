using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    private bool isMoving;
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) isMoving = true;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            isMoving = false;
            
            float xMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            float yMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
            
            Vector3 movement = new Vector3(xMovement, yMovement, 0);

            var playerTransform = transform;
            
            movement = playerTransform.rotation * movement;

            playerTransform.position += movement;
        }
    }
}
