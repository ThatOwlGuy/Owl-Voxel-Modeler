using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManagement
{
    private static string path = Application.persistentDataPath + "/Owl Voxel Models/";
    private static string currentOVM;

    public static void SaveOwlVoxelModel(OwlVoxelModel ovm, string name)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path + name + ".ovm", FileMode.CreateNew);

        binaryFormatter.Serialize(fileStream, ovm);
        fileStream.Close();
    }

    public static OwlVoxelModel LoadOwlVoxelModel(string name)
    {
        OwlVoxelModel loadedModel = null;

        if(File.Exists(path + name + ".ovm"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path + name + ".ovm", FileMode.Open);

            loadedModel = binaryFormatter.Deserialize(fileStream) as OwlVoxelModel;

            fileStream.Close();
        }
            
        return loadedModel;
    }

    public static string[] GetPathArrayOfOVMs()
    {
        return Directory.GetFiles(path, "*.ovm");
    }
}

[System.Serializable]
public class OwlVoxelModel
{
    OwlVoxel[] voxels;
}

[System.Serializable]
public struct OwlVoxel
{
    private int[] position;
    private int[] color;

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

    private int[] ValidateArray(int[] intArray)
    {
        if(intArray.Length == 0)
            intArray = new int[3];

        return intArray;
    }

    private int ValidateColor(int colorValue)
    {
        if(colorValue > 255)
            colorValue = 255;

        if(colorValue < 0)
            colorValue = 0;

        return colorValue;
    }
}