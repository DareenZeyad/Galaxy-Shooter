using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private GameObject _enemyExplosionPrefab;
    [SerializeField] private AudioClip _enemyExplosionClip;
    private UI_Manager _uiManager;

    void Start(){
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }

    void Update(){
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6.5f){
            float xPostition = Random.Range(-7.8f, 7.8f);
            Vector3 position = new Vector3(xPostition, 8.0f, 0);
            Instantiate(this.gameObject, position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            AudioSource.PlayClipAtPoint(_enemyExplosionClip, Camera.main.transform.position);
            Player player = other.GetComponent<Player>();
            player.Damage();
        }
        else if (other.tag == "Laser"){
            if (other.transform.parent != null){
                Destroy(other.transform.parent.gameObject);
            }
            _uiManager.UpdateScorse();
            AudioSource.PlayClipAtPoint(_enemyExplosionClip, Camera.main.transform.position);
            Destroy(other.gameObject);
        }
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
