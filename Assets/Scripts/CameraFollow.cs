using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Sets camera to follow player and spawn in correct locations in scenes
/// </summary>
public class CameraFollow : MonoBehaviour
{
    //Declare variables
    private Transform player;
    private Vector3 offset;
    Vector2 boundsMinSci;
    Vector2 boundsMaxSci;
    Vector2 boundsMaxGround;
    Vector2 boundsMinGround;

    void Start()
    {
        //set Camera bounds in ground floor scene and first floor scene
        boundsMinSci = new Vector2(-7.8f, -20.7f);
        boundsMaxSci = new Vector2(16.7f, 6.3f);
        boundsMinGround = new Vector2(-28.5f, -29f);
        boundsMaxGround = new Vector2(-2.8f, -1.5f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //if player in first floor scene...
        if (SceneManager.GetActiveScene().name == "Level One")
        {
            //if player is within camera bounds, camera should follow player
            if (player.position.x > boundsMinSci.x && player.position.x < boundsMaxSci.x)
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            }
            if (player.position.y > boundsMinSci.y && player.position.y < boundsMaxSci.y)
            {
                transform.position = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            }
        }

        //if player in ground floor scene...
        if (SceneManager.GetActiveScene().name == "Ground Floor")
        {
            //if player within bounds, camera should follow players
            if (player.position.x > boundsMinGround.x && player.position.x < boundsMaxGround.x)
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            }
            if (player.position.y > boundsMinGround.y && player.position.y < boundsMaxGround.y)
            {
                transform.position = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            }
        }

    }
}
