using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    private bool isMoving;

    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    
    private bool canShoot = true;
    
    private Camera _camera;
    
    private void Start()
    {
        currentHealth = maxHealth;
        _camera = Camera.main;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health: " + currentHealth);
    }
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) isMoving = true;
        
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
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
    
    private IEnumerator Shoot()
    {
        var cooldown = projectile.GetComponent<ProjectileController>().GetCooldown();
        
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Vector3 direction = mousePosition - transform.position;
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        
        Instantiate(projectile, projectileSpawnPoint.position, Quaternion.Euler(0, 0, angle));

        yield return new WaitForSeconds(cooldown);
        
        canShoot = true;
    }
}
