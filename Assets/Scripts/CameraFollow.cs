using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /* 
     * This class makes camera follow player 
     * */
    private Transform player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
