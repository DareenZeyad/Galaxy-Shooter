using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour{

    private Animator _animator;

    void Start(){
        _animator = GetComponent<Animator>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            _animator.SetBool("Turn_Left", true);
            _animator.SetBool("Turn_Right", false);
        }

        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
            _animator.SetBool("Turn_Right", false);
            _animator.SetBool("Turn_Left", false);
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            _animator.SetBool("Turn_Right", true);
            _animator.SetBool("Turn_Left", false);
        }

        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)){
            _animator.SetBool("Turn_Right", false);
            _animator.SetBool("Turn_Left", false);
        }
    }
}
