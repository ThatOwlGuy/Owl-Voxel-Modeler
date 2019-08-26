using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndSpin : MonoBehaviour
{
    [SerializeField] Transform objectToActUpon;

    public void Spin(float degrees)
    {
        objectToActUpon.localRotation = Quaternion.Euler(
            objectToActUpon.localRotation.eulerAngles.x,
            objectToActUpon.localRotation.eulerAngles.y + degrees,
            objectToActUpon.localRotation.eulerAngles.z
        );
    }

    public void Zoom(float amount)
    {
        objectToActUpon.localScale = objectToActUpon.localScale + (Vector3.one * amount * 0.1f);
    }
}
