using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    private bool isMoving;

    [SerializeField] private GameObject projectile;
    
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) isMoving = true;
        
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Shoot());
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
    
    private IEnumerator Shoot(float cooldown = 0.5f)
    {
        var playerTransform = transform;
        var playerPosition = playerTransform.position;
        var playerRotation = playerTransform.rotation;
        
        Instantiate(projectile, playerPosition, playerRotation);
        
        yield return new WaitForSeconds(cooldown);
    }
}
