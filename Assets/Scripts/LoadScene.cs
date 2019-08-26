using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadThisScene(string sceneInQuestion)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneInQuestion);
    }
}
