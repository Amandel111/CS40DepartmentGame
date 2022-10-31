using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls functionality for transitionaing between scenes
/// </summary>
public class TransitionScene : MonoBehaviour
{
    //declare variables
    public string levelToLoad;
    public Animator transitionAnim;
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if collision object is player...
        if (collision.gameObject.CompareTag("Player"))
        {
            //start LoadScene coroutine
            Debug.Log("collided w player");
            StartCoroutine(LoadScene());
            Debug.Log("Calls load scene");
        }
    }

    /// <summary>
    /// start transition scene animation, disable UI, and load Main Menu
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
        player.EnableDisableUI(false);
    }
}
