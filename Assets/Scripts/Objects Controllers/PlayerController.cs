using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerStats _playerStats;
    
    private bool isMoving;
    
    private int currentHealth;
    
    [SerializeField] private ProgressBar healthBar;
    
    [SerializeField] private Transform projectileSpawnPoint;
    
    private bool _canShoot = true;
    
    private Camera _camera;
    
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private List<Sprite> sprites;
    
    [SerializeField] private UIDocumentManager uiDocumentManager;
    
    [SerializeField] private SoundManager soundManager;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = _playerStats.MaxHealth;
        UpdateHealthbarScale();
        healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
        _camera = Camera.main;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            healthBar.SetProgress(0, 2);
            uiDocumentManager.GameOver(false);
        }
        else
        {
            healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
        }
        
    }

    public bool Heal(int heal)
    {
        if (currentHealth == _playerStats.MaxHealth) return false;

        currentHealth += heal;
        soundManager.PlayDrinkingSound();
        if (currentHealth > _playerStats.MaxHealth)
        {
            currentHealth = _playerStats.MaxHealth;
        }

        healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
        
        return true;
    }

    public void UpdateHealthbarScale()
    {
        healthBar.SetTextScale(_playerStats.MaxHealth);
        healthBar.SetProgress((float)currentHealth / _playerStats.MaxHealth, 2);
    }
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) isMoving = true;
        
        if (Input.GetKey(KeyCode.Space) && _canShoot)
        {
            _canShoot = false;
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

            playerTransform.position += movement;
            
            ChangeSprite(movement);
        }
    }
    
    private IEnumerator Shoot()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Vector3 direction = mousePosition - transform.position;
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        
        Instantiate(_playerStats.Projectile, projectileSpawnPoint.position, Quaternion.Euler(0, 0, angle));

        soundManager.PlayWhooshSound();
        
        yield return new WaitForSeconds(_playerStats.ShootingSpeed);
        
        _canShoot = true;
    }

    private void ChangeSprite(Vector3 direction)
    {
        //muda o sprite tendo em conta a direção (4 direções)
        
        if (direction.x > 0)
        {
            _spriteRenderer.sprite = sprites[0];
        }
        else if (direction.x < 0)
        {
            _spriteRenderer.sprite = sprites[1];
        }
        else if (direction.y > 0)
        {
            _spriteRenderer.sprite = sprites[2];
        }
        else if (direction.y < 0)
        {
            _spriteRenderer.sprite = sprites[3];
        }
    }
}
