using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public string levelToLoad;
    public Animator transitionAnim;
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided w player");
            StartCoroutine(LoadScene());
            Debug.Log("Calls load scene");
        }
    }


    IEnumerator LoadScene()
    {
        //play scene transition before loading next scene
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
        player.EnableDisableUI(false);
    }
}
