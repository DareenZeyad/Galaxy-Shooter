using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerUpID; // 0: Triple Shot, 1: Speed Boost, 2: Shields
    [SerializeField] private AudioClip _powerUpAudioClip;
    void Update(){
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y <= -6.8f){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player") {
            AudioSource.PlayClipAtPoint(_powerUpAudioClip, Camera.main.transform.position, 1.0f);

            Player player = other.GetComponent<Player>();
            if (player != null) {
                if (_powerUpID == 0)
                    player.TripleShotPowerUpOn();
                else if (_powerUpID == 1)
                    player.SpeedUpPowerUpOn();
                else if (_powerUpID == 2)
                    player.ShieldPowerUpOn();
            }
            Destroy(this.gameObject);
        }
    }
}
