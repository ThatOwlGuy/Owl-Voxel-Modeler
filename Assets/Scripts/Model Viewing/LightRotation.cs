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

    private Quaternion Down()
    {
        Vector3 eulerPhoneRotation = Input.gyro.attitude.eulerAngles;
        Vector3 differenceFromDown = Vector3.down - eulerPhoneRotation;
        return Quaternion.Euler(differenceFromDown);
    }
}
