using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls  main menu, loading levels,
/// exiting the application, and the corresponding UI
/// </summary>
public class MenuController : MonoBehaviour
{
    //declare variables
    public string levelToLoad;
    public Animator transitionAnim;
    private AudioSource music;
    private Animator musicAnim;
    SceneController tempLevelLoader;

    private void Start()
    {
        //initialize variables
        music = GetComponent<AudioSource>();
        musicAnim = GetComponent<Animator>();
        tempLevelLoader = FindObjectOfType<SceneController>();

        //delete previous game memory if it exists
        if (tempLevelLoader != null)
        {
            Destroy(tempLevelLoader);
        }
    }

    /// <summary>
    /// starts LoadScene coroutine
    /// </summary>
    public void LoadGameButtonYes()
    {
        //start game
        Debug.Log("game button pressed");
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// starts EndScene coroutine
    /// </summary>
    public void ExitButton()
    {
        StartCoroutine(EndScene());
    }

    /// <summary>
    /// triggers scene transition animation and exits game
    /// </summary>
    /// <returns></returns>
    IEnumerator EndScene()
    {
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(2);
        Application.Quit();

    }

    /// <summary>
    /// triggers scene transition animation and loads game
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("endTrigger");
        musicAnim.SetTrigger("fadeMusic");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
