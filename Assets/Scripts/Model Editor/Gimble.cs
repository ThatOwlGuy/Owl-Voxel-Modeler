using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gimble : MonoBehaviour
{
    [SerializeField] private Text rotateOrLocateText;
    private bool rotate = true;

    public void ToggleRotateOrLocate()
    {
        rotate = !rotate;

        rotateOrLocateText.text = rotate ? "Rotate" : "Locate";
    }

    private void Update()
    {
        if (OverUI())
            return;

        if (Input.GetMouseButtonDown(0))
            StartCoroutine(GimbleActinoRoutine());
    }

    private IEnumerator GimbleActinoRoutine()
    {
        Vector2 initialMousePosition = Input.mousePosition;
        Vector3 initialRotation = transform.rotation.eulerAngles;
        Vector3 initialPosition = transform.position;
        Vector3 newPosition = initialPosition;

        yield return new WaitForSeconds(0.25f);

        while (Input.GetMouseButton(0))
        {
            if (rotate)
            {
                Vector3 newRotation = initialRotation;
                
                newRotation = initialRotation + new Vector3
                (
                    (initialMousePosition.y - Input.mousePosition.y) * 0.1f,
                    (Input.mousePosition.x - initialMousePosition.x) * 0.1f,
                    0f
                );

                newRotation.x = newRotation.x < 5f ? 5f : newRotation.x;
                newRotation.x = newRotation.x > 60f ? 60f : newRotation.x;

                transform.rotation = Quaternion.Euler(newRotation);
            }
            else
            {
                Vector3 newForward = FindObjectOfType<Camera>().transform.forward;
                Vector3 newRight = FindObjectOfType<Camera>().transform.right;
                newForward.y = newRight.y = 0.0f;
                newForward = newForward.normalized;
                newRight = newRight.normalized;

                newPosition = initialPosition + 
                    (newForward * (initialMousePosition.y - Input.mousePosition.y) * 0.01f) +
                    (newRight * (initialMousePosition.x - Input.mousePosition.x) * 0.01f);

                transform.position = newPosition;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private bool OverUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }
}
