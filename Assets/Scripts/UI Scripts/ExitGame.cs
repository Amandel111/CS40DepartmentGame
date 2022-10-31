using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public string levelToLoad;
    public Animator transitionAnim;
    private GameObject exitPanel;
    // Start is called before the first frame update
    private void Start()
    {
        exitPanel = GameObject.FindGameObjectWithTag("exitPanel");
        exitPanel.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exitPanel.SetActive(true);
        }
    }
    public void ExitGamePromptYes()
    {
            exitPanel.SetActive(false);;
            StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        //play scene transition before loading next scene
        transitionAnim.SetTrigger("endTrigger");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
        //SceneManager.LoadScene(levelToLoad);
    }
}
