using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour{

    [SerializeField] private GameObject _enemyShipPrefab;
    [SerializeField] private GameObject[] _powerUpPrefabs;
    private GameManager _gameManager;

    void Start(){
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawnRoutine()); 
        StartCoroutine(PowerUpSpawnRoutine());

    }
    
    public void StartSpawnRoutine(){
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine(){
        while (_gameManager.gameOver == false) {
            float xPostition = Random.Range(-7.8f, 7.8f);
            Vector3 position = new Vector3(xPostition, 8.0f, 0);
            Instantiate(_enemyShipPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerUpSpawnRoutine(){
        while (_gameManager.gameOver == false){
            // Random Range with ints: (min, max - 1)
            int randomNum = Random.Range(0, 3);
            float xPostition = Random.Range(-7.8f, 7.8f);
            Vector3 position = new Vector3(xPostition, 8.0f, 0);
            Instantiate(_powerUpPrefabs[randomNum], position, Quaternion.identity);
            yield return new WaitForSeconds(6.0f);
        }
    }
}
