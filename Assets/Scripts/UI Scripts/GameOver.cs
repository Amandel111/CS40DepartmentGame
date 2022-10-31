using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls functionality if player gets a game over
/// </summary>
public class GameOver : MonoBehaviour
{
    //declare variables
    public string levelToLoad;
    public Animator transitionAnim;

    /// <summary>
    /// starts LoadScene coroutine
    /// </summary>
    public void ExitGamePromptYes()
    {
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// starts transition scene animation and loads Main Menu
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
