using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoxelDisplay : MonoBehaviour
{
    [SerializeField] private GameObject voxel;

    private void Start()
    {
        DisplayCurrentVoxelModel();
        FindObjectOfType<InputField>().text = FileManagement.currentOVM;
    }

    public void DisplayCurrentVoxelModel()
    {
        DisplayVoxelModel(FileManagement.LoadCurrentOVM());
    }

    public void DisplayVoxelModel(OwlVoxelModel ovm)
    {
        if(ovm == null)
            return;

        foreach(OwlVoxel voxel in ovm.voxels)
        {
            CreateVoxel(voxel);
        }
    }

    private void CreateVoxel(OwlVoxel voxel)
    {
        GameObject newVoxel = Instantiate(this.voxel, Vector3.zero, Quaternion.identity);

        newVoxel.transform.SetParent(transform);

        newVoxel.transform.localScale = Vector3.one;

        newVoxel.transform.localPosition = new Vector3(voxel.x, voxel.y, voxel.z);
    }

    public OwlVoxelModel GenerateOVM()
    {
        OwlVoxelModel newOVM = new OwlVoxelModel();
        List<OwlVoxel> voxels = new List<OwlVoxel>();

        foreach(Transform child in transform)
        {
            OwlVoxel newVoxel = new OwlVoxel();
            newVoxel.x = (int)child.localPosition.x;
            newVoxel.y = (int)child.localPosition.y;
            newVoxel.z = (int)child.localPosition.z;

            voxels.Add(newVoxel);
        }

        newOVM.AddVoxels(voxels.ToArray());

        return newOVM;
    }
}
