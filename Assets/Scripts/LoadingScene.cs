using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingBaFill;
    public GameObject button;
    public GameObject slider;
    public void LoadScene(int sceneId)
    {
        Debug.Log("Test");
        button.SetActive(false);
        slider.SetActive(true);
        StartCoroutine(LoadSceneAsnc(sceneId));
    }

    IEnumerator LoadSceneAsnc(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        LoadingScreen.SetActive(true);

        while (!operation.isDone) 
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBaFill.value = progressValue;
        
            yield return null;        
        }
    }
}
