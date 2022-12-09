using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private AudioClip dingSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject lasers;
    [SerializeField] private GameObject laserParticles;
    [SerializeField] GameObject bombExplosion;
    public CharacterSelection characterSelection;
    public float laserInterval = 0.45f;
    public float moveSpeed;
    public float xRange;
    public float yRange;
    public float posPitchFactor = -2f;
    public float posYawFactor = 2f;
    public float controlPitchFacor = -10f;
    public float controlRollFactor = -20f;
    private float xThrow;
    private float yThrow;
    private float laserTimer;
    public bool isLaserReady = true;
    public int civsSaved = 0;
    public float bombsDestroyed = 0;
    public float civsKilled = 0;
    public int bombsCollided = 0;
    public int bombsMissed = 0;
    public int civsMissed = 0;
    public float gameTimer = 0;
    public GameObject Glacier;
    public GameObject Pirate;
    public GameObject BigBrain;

    private void Start()
    {
        BigBrain.SetActive(false);
        characterSelection = GameObject.FindGameObjectWithTag("Persist").GetComponent<CharacterSelection>();
        if (characterSelection.selectedCharacter.name == "Pirate")
        {
            Pirate.SetActive(true);
        }
        else if (characterSelection.selectedCharacter.name == "Glacier")
        {
            Glacier.SetActive(true);
        }
        else if (characterSelection.selectedCharacter.name == "BigBrain")
        {
            BigBrain.SetActive(true);
        }
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ShootLasers();
        if (!isLaserReady)
        {
            LaserTimer();
        }
    }

    private void LaserTimer()
    {
        laserTimer += Time.deltaTime;
        if (laserTimer >= laserInterval)
        {
            isLaserReady = true;
            laserTimer = 0;          
        }
    }

    private void ShootLasers()
    {
        if (Input.GetKey(KeyCode.Space) && isLaserReady)
        {
            audioSource.PlayOneShot(laserSound);
            GameObject newLaser = Instantiate(lasers);
            newLaser.transform.position = head.transform.position;
            newLaser.transform.eulerAngles = transform.eulerAngles;
            PlayLaserParticles(newLaser.transform);
            isLaserReady = false;
        }
    }

    private void PlayLaserParticles(Transform laserTrasform)
    {

        GameObject particleSystemObject1 = Instantiate(laserParticles.transform.GetChild(0).gameObject);
        particleSystemObject1.transform.position = laserTrasform.position + new Vector3(-0.1f, 0.1f, 0.1f);
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Root"))
            {
                particleSystemObject1.transform.parent = child;
            }
        }
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float yOffset = yThrow * Time.deltaTime * moveSpeed;
        float xOffset = xThrow * Time.deltaTime * moveSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, 1, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y + posPitchFactor;
        float pitchDueToControlThrow = yThrow + controlPitchFacor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * posYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Civ")
        {
            audioSource.PlayOneShot(dingSound);
            civsSaved += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Bomb")
        {
            audioSource.PlayOneShot(explosionSound);
            GameObject newSparks = Instantiate(bombExplosion);
            newSparks.transform.position = collision.GetContact(0).point;
            newSparks.transform.parent = collision.transform;
            collision.transform.GetComponent<SphereCollider>().enabled = false;
            collision.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            bombsCollided += 1;
        }
    }

    public void CreateDataFile()
    {
        if (!Directory.Exists("C:\\Data\\TakeFlight"))
        {

            Directory.CreateDirectory("C:\\Data\\TakeFlight");
        }

        string civsSavedText = null;
        if (civsSaved <= 9)
        {
            civsSavedText = "00" + civsSaved.ToString();
        }
        else if (civsSaved >= 10 && civsSaved <= 99)
        {
            civsSavedText = "0" + civsSaved.ToString();
        }
        else if (civsSaved >= 100)
        {
            civsSavedText = civsSaved.ToString();
        }

        string civsNotSavedText = null;
        int civsNotSavedTotal = (int)(civsKilled + civsMissed);
        if (civsNotSavedTotal <= 9)
        {
            civsNotSavedText = "00" + civsNotSavedTotal.ToString();
        }
        else if (civsNotSavedTotal >= 10 && civsNotSavedTotal <= 99)
        {
            civsNotSavedText = "0" + civsNotSavedTotal.ToString();
        }
        else if (civsNotSavedTotal >= 100)
        {
            civsNotSavedText = civsNotSavedTotal.ToString();
        }

        string bombsDestroyedText = null;
        int totalBombsDestroyed = (int)(bombsCollided + bombsDestroyed);
        if (totalBombsDestroyed <= 9)
        {
            bombsDestroyedText = "00" + totalBombsDestroyed.ToString();
        }
        else if (totalBombsDestroyed >= 10 && totalBombsDestroyed <= 99)
        {
            bombsDestroyedText = "0" + totalBombsDestroyed.ToString();
        }
        else if (totalBombsDestroyed >= 100)
        {
            bombsDestroyedText = totalBombsDestroyed.ToString();
        }

        string bombsNotDestroyedText = null;
        if (bombsMissed <= 9)
        {
            bombsNotDestroyedText = "00" + bombsMissed.ToString();
        }
        else if (totalBombsDestroyed >= 10 && totalBombsDestroyed <= 99)
        {
            bombsNotDestroyedText = "0" + bombsMissed.ToString();
        }
        else if (totalBombsDestroyed >= 100)
        {
            bombsNotDestroyedText = bombsMissed.ToString();
        }

        string characterIDText = null;
        if (BigBrain.activeSelf)
        {
            characterIDText = 2.ToString();
        }
        else if (Pirate.activeSelf)
        {
            characterIDText = 1.ToString();
        }
        else if (Glacier.activeSelf)
        {
            characterIDText = 5.ToString();
        }

        File.WriteAllText("C:\\Data\\TakeFlight\\stats.txt", characterIDText + civsSavedText + civsNotSavedText + bombsDestroyedText + bombsNotDestroyedText);
        Debug.Log("Data file output");
    }
}
