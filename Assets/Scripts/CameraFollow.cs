using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    /* 
     * This class makes camera follow player 
     * */
    private Transform player;
    private Vector3 offset;
    Vector2 boundsMinSci;
    Vector2 boundsMaxSci;
    Vector2 boundsMaxGround;
    Vector2 boundsMinGround;

    void Start()
    {
        boundsMinSci = new Vector2(-7.8f, -20.7f);
        boundsMaxSci = new Vector2(16.7f, 6.3f);
        boundsMinGround = new Vector2(-28.5f, -29f);
        boundsMaxGround = new Vector2(-2.8f, -1.5f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        /*
        if (LevelOneController.previousScene == "Level One" && SceneManager.GetActiveScene().name == "Ground Floor")
        {
            Debug.Log("come from level 1");
            transform.position = new Vector3(-21f, -2, transform.position.z);
        }
        */
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level One")
        {
            if (player.position.x > boundsMinSci.x && player.position.x < boundsMaxSci.x)
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            }
            if (player.position.y > boundsMinSci.y && player.position.y < boundsMaxSci.y)
            {
                transform.position = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            }
        }
        if (SceneManager.GetActiveScene().name == "Ground Floor")
        {
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
