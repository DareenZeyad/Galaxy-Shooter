using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool gameOver = true;
    [SerializeField] private GameObject _playerPrefab;
    private UI_Manager _uiManager;

    private void Start(){
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>(); 
    }

    private void Update(){
        if (gameOver == true) {
            if (Input.GetKeyDown(KeyCode.Space)){
                Instantiate(_playerPrefab, new Vector3(0, -3.6f, 0), Quaternion.identity);
                _uiManager.HideTitleScreen();
                gameOver = false;
            }       
        }
    }
}
