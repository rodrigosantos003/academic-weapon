using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    private int maxHealth = 200;
    private int currentHealth;
    
    private static GameObject _healthBarPrefab;
    private static Canvas _healthBarCanvas;
    private ProgressBar _healthBar;
    private GameObject _healthBarObject;

    private static GameObject _player;
    
    private static GameManager _gameManager;
    
    private static SoundManager _soundManager;
    
    public static void SetSoundManager(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }
    
    public static void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public static void SetHealthBarCanvas(Canvas canvas)
    {
        _healthBarCanvas = canvas;
    }
    
    public static void SetHealthBarPrefab(GameObject prefab)
    {
        _healthBarPrefab = prefab;
    }
    
    public static void SetPlayer(GameObject player)
    {
        _player = player;
    }
    
    private void SpawnHealthBar()
    {
        var healthBarPosition = transform.position + new Vector3(0, 1, 0);
        _healthBarObject = Instantiate(_healthBarPrefab, healthBarPosition, Quaternion.identity, _healthBarCanvas.transform);
        _healthBar = _healthBarObject.GetComponent<ProgressBar>();
        _healthBar.SetProgress(currentHealth / maxHealth, 2);
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
        SpawnHealthBar();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= (int)(damage * (1 - (float)stats.Defense / 100));

        if (currentHealth <= 0)
        {
            Die();
            
            _soundManager.PlayPaperSound();
            
            if (Random.Range(0, 5) == 0)
                _gameManager.SpawnCoffee(transform.position);
        }
        else
            _healthBar.SetProgress((float)currentHealth / maxHealth, 2);
    }

    public void Die()
    {
        Destroy(gameObject);
        Destroy(_healthBarObject);
    }
    
    private void KeepHealthBarOnTop()
    {
        var healthBarPosition = transform.position + new Vector3(0, 1, 0);
        _healthBarObject.transform.position = healthBarPosition;
    }
    private void FixedUpdate()
    {
        if (_player)
        {
            transform.position += (_player.transform.position - transform.position).normalized * (stats.Speed * Time.deltaTime);
            KeepHealthBarOnTop();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(currentHealth);
            Die();
        }
    }
}
