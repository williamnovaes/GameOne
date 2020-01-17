using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroTransition : MonoBehaviour
{
    private Animator introAnim;
    void Start()
    {
       StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("StartMenu");
    }
}
