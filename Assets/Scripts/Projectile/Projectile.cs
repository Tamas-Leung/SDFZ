using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float damage { get; private set; }
    public Type damageType { get; private set; }
    public bool IsEnemy;
    public AudioClip audioClip;
    private AudioSource audioSource;

    public void Setup(Vector3 direction, float damage, Type damageType)
    {
        AudioSource audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        this.damage = damage;
        this.damageType = damageType;
        this.SetupDirection(direction);
        if (audioClip)
            audioSource.PlayOneShot(audioClip, Random.Range(0.5f, 0.8f));
    }

    abstract public void SetupDirection(Vector3 direction);
}
