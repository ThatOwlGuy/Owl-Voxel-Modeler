using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FMButtonCommunicator : MonoBehaviour
{
    public void SetCurrentOVM(string newOVM)
    {
        FileManagement.SetCurrentOVM(newOVM);
    }

    public void ClearCurrentOVM()
    {
        FileManagement.currentOVM = "";
    }

    public void DisplyCurrntOVM()
    {
        FindObjectOfType<VoxelDisplay>().DisplayVoxelModel(FileManagement.LoadCurrentOVM());
    }

    public void SaveOwlVoxelModel(OwlVoxelModel ovm, string name)
    {
        FileManagement.SaveOwlVoxelModel(
            FindObjectOfType<VoxelDisplay>().GenerateOVM(),
            FindObjectOfType<InputField>().text
        );
    }

    public OwlVoxelModel LoadOwlVoxelModel(string name)
    {
        return FileManagement.LoadOwlVoxelModel(name);
    }

    public string[] GetPathArrayOfOVMs()
    {
        return FileManagement.GetPathArrayOfOVMs();
    }

    public bool ModelExists(string modelName)
    {
        return FileManagement.ModelExists(modelName);
    }
}
