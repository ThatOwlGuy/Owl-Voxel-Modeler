using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelTool : MonoBehaviour
{
    public bool inUse;

    public virtual void Update()
    {
        if(!inUse)
            return;

        if(Input.GetMouseButtonDown(0))
            ToolFunction();
    }

    protected virtual void ToolFunction()
    {
        print("This is the virtual (base) method of a Tool Function");
    }
}
