using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffects : MonoBehaviour
{
    void Start(){
        Destroy(this.gameObject, 4.0f);
    }
}
