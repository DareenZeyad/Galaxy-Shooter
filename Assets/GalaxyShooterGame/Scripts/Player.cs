using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _speedUp = 5.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _shieldsPrefab;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject[] _engineFailure;

    public int playerLife = 3;
    private int _engineFailureNum;
    private float _canFire = 0.0f;
    public bool canTripleShot = false;
    public bool canSpeedUp = false;
    public bool canShield = false;
    private UI_Manager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    void Start() {
        transform.position = new Vector3(0, -3.6f, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager != null)
            _spawnManager.StartSpawnRoutine();
        _engineFailureNum = 0;
    }

    void Update() {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            Shot();
    }

    private void Movement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedUp == true) {
            transform.Translate(Vector3.right * horizontalInput * _speedUp * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speedUp * Time.deltaTime);
        }
        else {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }

        if (transform.position.x > 9.5f)
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        else if (transform.position.x < -9.5f)
            transform.position = new Vector3(9.5f, transform.position.y, 0);

        if (transform.position.y > 0)
            transform.position = new Vector3(transform.position.x, 0, 0);
        if (transform.position.y < -4.0f)
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
    }

    private void Shot() {
        if (Time.time > _canFire) {
            _audioSource.Play();
            if (canTripleShot == true) {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else {
                Vector3 laserNewLocation = transform.position + new Vector3(0, 1.1f, 0);
                Instantiate(_laserPrefab, laserNewLocation, Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    public void Damage(){
        Debug.Log(playerLife);
        
        if (canShield != true){
            playerLife--;
            _uiManager.UpdateLives(playerLife);

            _engineFailureNum++;
            if (_engineFailureNum == 1)
                _engineFailure[0].SetActive(true);

            else if (_engineFailureNum == 2)
                _engineFailure[1].SetActive(true);
        }
        if (playerLife == 0) {
            playerLife = 3;
            if (canShield == false){
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _gameManager.gameOver = true;
                _uiManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }
        }
    }

    // --- Triple Shot Coroutine ---
    public void TripleShotPowerUpOn(){
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    /// Using Coroutine: After 5 second to disable the triple shoting
    public IEnumerator TripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }


    // --- Speed Up Boost Coroutine ---
    public void SpeedUpPowerUpOn(){
        canSpeedUp = true;
        StartCoroutine(SpeedUpPowerDownRoutine());
    }
    public IEnumerator SpeedUpPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        canSpeedUp = false;
    }

    // --- Sheild Coroutine ---
    public void ShieldPowerUpOn(){
        canShield = true;
        _shieldsPrefab.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    public IEnumerator ShieldPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        canShield = false;
        _shieldsPrefab.SetActive(false);
    }
}
