using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _audioSource;
    [Header("Audio Settings")]
    [SerializeField] private AudioClip _explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource= GetComponent<AudioSource>();

        _audioSource.PlayOneShot(_explosionSound);
    }
}
