using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * Handles instantiation, positioning, and translation of game tiles.
 * Bret Shepard
*/

public class GroundGenerator : MonoBehaviour
{
    public DecreaseTime decreaseTime;
    public Camera mainCamera;
    public Transform startPoint; //Point from where ground tiles will start
    public PlatformTile tilePrefab;
    public float movingSpeed = 12;
    public int tilesToPreSpawn = 15; //How many tiles should be pre-spawned

    List<PlatformTile> spawnedTiles = new List<PlatformTile>();
    int nextTileToActivate = -1;
    [HideInInspector]
    public bool gameOver = false;
    static bool gameStarted = false;
    float score = 0;

    public static GroundGenerator instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; // This line of code is here to make sure that only one instance of GroundGenerator exist

        Vector3 spawnPosition = startPoint.position; 
        for (int i = 0; i < tilesToPreSpawn; i++) // Going to spawn these tiles
        {
            spawnPosition -= tilePrefab.startPoint.localPosition; // Gets how far the origin of the tile should be away from the start position
            PlatformTile spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as PlatformTile;          
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        // Move the object upward in world space x unit/second.
        //Increase speed the higher score we get
        if (!gameOver && gameStarted)
        {
            transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * (movingSpeed + (score / 500)), Space.World);
            score += Time.deltaTime * movingSpeed;
        }

        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            PlatformTile tileTmp = spawnedTiles[0];
            tileTmp.GetComponent<MissedObjectsCounter>().MissedItemsCount();
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            spawnedTiles.Add(tileTmp);
        }

        if (gameOver || !gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameOver)
                {
                    //Restart current scene
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                else
                {
                    //Start the game
                    decreaseTime.enabled = true;
                    gameStarted = true;
                }
            }
        }
    }
}
