using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// controls in-game quitting application
/// </summary>
public class ExitGame : MonoBehaviour
{
    //declare variables
    public string levelToLoad;
    public Animator transitionAnim;
    private GameObject exitPanel;

    private void Start()
    {
        //initialize variables
        exitPanel = GameObject.FindGameObjectWithTag("exitPanel");

        //only want exitPanel UI triggered if conditions met
        exitPanel.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if collision object is player...
        if (collision.gameObject.CompareTag("Player"))
        {
            //set exitPanel UI active
            exitPanel.SetActive(true);
        }
    }

    /// <summary>
    /// turns off exitPanel UI and starts LoadScene coroutine
    /// </summary>
    public void ExitGamePromptYes()
    {
            exitPanel.SetActive(false);;
            StartCoroutine(LoadScene());
    }

    /// <summary>
    /// sets scene transition animation and quits game
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        //play scene transition before loading next scene
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
