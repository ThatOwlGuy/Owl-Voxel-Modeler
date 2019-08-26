using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FileViewObject : MonoBehaviour
{
    private bool editingFileName;
    public string fileName
    {
        get
        {
            return transform.GetChild(0).GetComponent<Text>().text;
        }

        set
        {
            if(!FileManagement.ModelExists(value))
                return;

            transform.GetChild(0).GetComponent<Text>().text = value;
        }
    }
    

    public void EditModel()
    {
        FileManagement.currentOVM = fileName;

        SceneManager.LoadScene("Voxel Editor");
    }

    public void ViewModel()
    {
        FileManagement.currentOVM = fileName;

        SceneManager.LoadScene("Voxel Model Viewer");
    }
}
