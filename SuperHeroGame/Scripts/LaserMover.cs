using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hurtCiv;
    public AudioClip explosionSound;
    [SerializeField] GameObject laserHitSparks;
    [SerializeField] GameObject bombExplosion;
    private float laserSpeed = 100;
    private float laserDestroyTimer;
    public PlayerMovement player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {      
        GetComponent<Rigidbody>().AddForce(transform.forward * laserSpeed, ForceMode.Force);
        laserDestroyTimer += Time.deltaTime;
        if (laserDestroyTimer >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Building")
        {
            GameObject newSparks = Instantiate(laserHitSparks);
            newSparks.transform.position = collision.GetContact(0).point;
            newSparks.transform.parent = collision.transform;
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "Bomb")
        {
            audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(explosionSound);
            GameObject newSparks = Instantiate(bombExplosion);
            newSparks.transform.position = collision.GetContact(0).point;
            newSparks.transform.parent = collision.transform;
            collision.transform.GetComponent<SphereCollider>().enabled = false;
            collision.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            player.bombsDestroyed += 0.5f;
        }
        else if (collision.transform.tag == "Civ")
        {
            audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(hurtCiv);
            GameObject newSparks = Instantiate(bombExplosion);
            newSparks.transform.position = collision.GetContact(0).point;
            newSparks.transform.parent = collision.transform;
            collision.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            collision.transform.GetComponent<CapsuleCollider>().enabled = false;
            player.civsKilled += 0.5f;
        }
    }
}
