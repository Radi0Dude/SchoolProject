using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneB : MonoBehaviour
{
    [Scene]
    [SerializeField]
    string goToSceneName;

    public void OnButton()
    {
        SceneManager.LoadScene(goToSceneName);
    }
}
