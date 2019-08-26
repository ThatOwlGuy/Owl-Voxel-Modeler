using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public static class FileManagement
{
    private static string path = Application.persistentDataPath + "/Owl Voxel Models/";
    public static string currentOVM;

    public static void SetCurrentOVM(string newOVM)
    {
        if(File.Exists(path + newOVM + ".ovm"))
        {
            currentOVM = newOVM;
        }
    }

    public static OwlVoxelModel LoadCurrentOVM()
    {
        if(currentOVM == null || currentOVM.Length == 0)
            return null;

        return LoadOwlVoxelModel(currentOVM);
    }

    public static void SaveOwlVoxelModel(OwlVoxelModel ovm, string name)
    {
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path + name + ".ovm", FileMode.OpenOrCreate);

        binaryFormatter.Serialize(fileStream, ovm);
        fileStream.Close();

        SetCurrentOVM(name);
    }

    public static OwlVoxelModel LoadOwlVoxelModel(string name)
    {
        if(File.Exists(path + name + ".ovm"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path + name + ".ovm", FileMode.Open);

            OwlVoxelModel loadedModel = binaryFormatter.Deserialize(fileStream) as OwlVoxelModel;

            fileStream.Close();
            return loadedModel;
        }
            
        return null;
    }

    public static string[] GetPathArrayOfOVMs()
    {
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return Directory.GetFiles(path, "*.ovm");
    }

    public static bool ModelExists(string modelName)
    {
        return File.Exists(path + modelName + ".ovm");
    }
}

[System.Serializable]
public class OwlVoxelModel
{
    public OwlVoxel[] voxels;

    public void AddVoxel(OwlVoxel voxel)
    {
        OwlVoxel[] lonelyArray = {voxel};
        AddVoxels(lonelyArray);
    }

    public void AddVoxels(OwlVoxel[] newVoxels)
    {
        List<OwlVoxel> voxelList = voxels == null ? new List<OwlVoxel>() : new List<OwlVoxel>(this.voxels);

        voxelList.AddRange(new List<OwlVoxel>(newVoxels));

        this.voxels = voxelList.ToArray();
    }
}

[System.Serializable]
public struct OwlVoxel
{
    private int[] position;
    //private int[] color;

    public int x
    {
        get
        {
            return position.Length > 0 ?  position[0] : 0;
        }

        set
        {
            position = ValidateArray(position);
            position[0] = value;
        }
    }

    public int y
    {
        get
        {
            return position.Length > 0 ?  position[1] : 0;
        }

        set
        {
            position = ValidateArray(position);
            position[1] = value;
        }
    }

    public int z
    {
        get
        {
            return position.Length > 0 ?  position[2] : 0;
        }

        set
        {
            position = ValidateArray(position);
            position[2] = value;
        }
    }

/*
    public int r
    {
        get
        {
            return color.Length > 0 ?  color[0] : 0;
        }

        set
        {
            color = ValidateArray(color);
            color[0] = ValidateColor(value);
        }
    }

    public int g
    {
        get
        {
            return color.Length > 0 ?  color[1] : 0;
        }

        set
        {
            color = ValidateArray(color);
            color[1] = ValidateColor(value);
        }
    }

    public int b
    {
        get
        {
            return color.Length > 0 ?  color[2] : 0;
        }

        set
        {
            color = ValidateArray(color);
            color[2] = ValidateColor(value);
        }
    }
*/
    private int[] ValidateArray(int[] intArray)
    {
        if(intArray == null)
            intArray = new int[3];

        return intArray;
    }

    /*private int ValidateColor(int colorValue)
    {
        if(colorValue > 255)
            colorValue = 255;

        if(colorValue < 0)
            colorValue = 0;

        return colorValue;
    }*/
}