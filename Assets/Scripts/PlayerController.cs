using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerStats _playerStats;
    
    private bool isMoving;
    
    private int currentHealth;
    
    [SerializeField] private ProgressBar healthBar;
    
    [SerializeField] private Transform projectileSpawnPoint;
    
    private bool canShoot = true;
    
    private Camera _camera;
    
    private void Start()
    {
        currentHealth = _playerStats.MaxHealth;
        UpdateHealthbarScale();
        healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
        _camera = Camera.main;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage * (1 - _playerStats.Defense / 100);
        healthBar.SetTextScale(_playerStats.MaxHealth);
        healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
    }

    public void UpdateHealthbarScale()
    {
        healthBar.SetTextScale(_playerStats.MaxHealth);
        healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
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
            
            float xMovement = Input.GetAxisRaw("Horizontal") * _playerStats.Speed * Time.deltaTime;
            float yMovement = Input.GetAxisRaw("Vertical") * _playerStats.Speed * Time.deltaTime;
            
            Vector3 movement = new Vector3(xMovement, yMovement, 0);

            var playerTransform = transform;
            
            movement = playerTransform.rotation * movement;

            playerTransform.position += movement;
        }
    }
    
    private IEnumerator Shoot()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Vector3 direction = mousePosition - transform.position;
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        
        Instantiate(_playerStats.Projectile, projectileSpawnPoint.position, Quaternion.Euler(0, 0, angle));

        yield return new WaitForSeconds(_playerStats.ShootingSpeed);
        
        canShoot = true;
    }
}
