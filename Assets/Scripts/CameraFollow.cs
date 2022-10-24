using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /* 
     * This class makes camera follow player 
     * */
    private Transform player;
    private Vector3 offset;
    Vector2 boundsMin;
    Vector2 boundsMax;

    void Start()
    {
        boundsMin = new Vector2(-7.8f, -20.7f);
        boundsMax = new Vector2(16.7f, 6.3f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (player.position.x > boundsMin.x && player.position.x < boundsMax.x)
        {
            if (player.position.y > boundsMin.y && player.position.y < boundsMax.y){
                transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }
        }
    }
}
