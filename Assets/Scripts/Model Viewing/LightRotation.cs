using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{

    public void LateUpdate()
    {
        if(!Input.isGyroAvailable)
            return;

        transform.rotation = Down();
    }

    private void Down()
    {
        Quaternion phoneRotation = Input.gryo.attitude;
        Quaternion differenceFromDown = Vector3.down - phoneRotation;
        return differenceFromDown;
    }
}
