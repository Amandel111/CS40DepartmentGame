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

    private void Start()
    {


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
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        //play scene transition before loading next scene
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
