using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /* This script will be controlling our main menu, loading levels,
     * exiting the application, and the graphics that go along with each
     */
    public string levelToLoad;
    public Animator transitionAnim;
    private AudioSource music;
    private Animator musicAnim;
    SceneController tempLevelLoader;

    private void Start()
    {
        music = GetComponent<AudioSource>();
        musicAnim = GetComponent<Animator>();
        tempLevelLoader = FindObjectOfType<SceneController>();
        if (tempLevelLoader != null)
        {
            Destroy(tempLevelLoader);
        }
    }
    public void LoadGameButtonYes()
    {
        //start game
        Debug.Log("game button pressed");
        StartCoroutine(LoadScene());
    }

    public void ExitButton()
    {
        //quit game
        StartCoroutine(EndScene());
    }

    IEnumerator EndScene()
    {
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(2);
        Application.Quit();

    }

    IEnumerator LoadScene()
    {
        //play scene transition before loading next scene
        transitionAnim.SetTrigger("endTrigger");
        musicAnim.SetTrigger("fadeMusic");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
