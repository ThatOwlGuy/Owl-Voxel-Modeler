using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveOVM : MonoBehaviour
{
    [SerializeField] private InputField ovmName;
    [SerializeField] private VoxelDisplay display;

    public void Save()
    {
        string ovmToSave = ovmName.text;

        if(ovmName.text.Length == 0)
            if(FileManagement.currentOVM.Length == 0)
                return;
            else
                ovmToSave = FileManagement.currentOVM;

        FileManagement.SaveOwlVoxelModel(display.GenerateOVM(), ovmToSave);
    }
}
