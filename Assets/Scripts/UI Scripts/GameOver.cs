using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public string levelToLoad;
    public Animator transitionAnim;
    public void ExitGamePromptYes()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        //play scene transition before loading next scene
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
