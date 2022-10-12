using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    [SerializeField]
    private AudioSource soundFX;

    [SerializeField]
    private AudioClip clipLand, clipFall;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void LandSound() {
        soundFX.clip = clipLand;
        soundFX.Play();
    }

    public void FallSound() {
        soundFX.clip = clipFall;
        soundFX.Play();
    }
}
