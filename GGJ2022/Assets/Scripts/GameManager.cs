using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields for the obstacles
    [SerializeField] private List<GameObject> obstaclePrefabs = new List<GameObject>();
    public static List<GameObject> obstacles = new List<GameObject>();
    private float obstacleSpawnX = 70f;
    private float obstacleSpawnY = -0.5f;
    private int maxObstacles = 10;

    // Fields for the robots
    private static GameObject[] robots = new GameObject[3];
    private static int robotIndex = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Get all the robots and set the Tackle Bot to be active
        robots = GameObject.FindGameObjectsWithTag("Player");
        robots[0].SetActive(false);
        robots[1].SetActive(false);
        
        // Spawn 'maxObstacles' obstacles initially
        for(int i = 0; i < maxObstacles; i++)
		{
            SpawnObstacle(18f + (8 * i));
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacles.Count > 0)
        {
            // When an obstacle goes off screen:
            // 1. Destroy it
            // 2. Spawn a new obstacle at the end of the line
            // 3. Increase the speed of all the obstacles
            if (obstacles[0].transform.position.x < (-Camera.main.aspect * Camera.main.orthographicSize) - obstacles[0].transform.localScale.x)
            {
                GameObject obstacleToDestroy = obstacles[0];
                obstacles.RemoveAt(0);
                Destroy(obstacleToDestroy);
                SpawnObstacle(obstacleSpawnX);
                foreach (GameObject obstacle in obstacles)
                {
                    // TODO - increase the speed of the obstacles after each destroyed obstacle
                }
            }
        }
    }

    /// <summary>
    /// Will poll the input system to see if the user has
    /// pressed one of the buttons to switch robots. Cycles
    /// through the robot list if they have.
    /// </summary>
    public static void TrySwitchRobots()
	{
        // The current robots position
        Vector2 oldPosition = robots[robotIndex].transform.position;
        // Cycle left one robot
        if (Input.GetKeyDown(KeyCode.Q))
        {
            robots[robotIndex].SetActive(false);
            if (robotIndex - 1 < 0)
            {
                robotIndex = robots.Length - 1;
            }
            else
            {
                robotIndex--;
            }
            robots[robotIndex].SetActive(true);
        }
        // Cycle right one robot
        else if (Input.GetKeyDown(KeyCode.E))
        {
            robots[robotIndex].SetActive(false);
            if (robotIndex + 1 > robots.Length - 1)
            {
                robotIndex = 0;
            }
            else
            {
                robotIndex++;
            }
            robots[robotIndex].SetActive(true);
        }
        // Set the new robot's position to be where the old one was
        robots[robotIndex].transform.position = oldPosition;
    }

    /// <summary>
    /// Spawns an obstacle at the given x-coordinate. The y-coordinate is determined based on which obstacle spawns.
    /// </summary>
    /// <param name="spawnX">The x-coordinate to spawn the obstacle</param>
    private void SpawnObstacle(float spawnX)
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Count);
        obstacles.Add(Instantiate(obstaclePrefabs[randomIndex], new Vector2(spawnX, obstacleSpawnY * randomIndex), Quaternion.identity));
    }
}
