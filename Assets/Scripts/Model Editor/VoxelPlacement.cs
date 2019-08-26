using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VoxelPlacement : MonoBehaviour 
{
    [SerializeField] private GameObject voxel;
    [SerializeField] private Text placeOrEraseText;
    private bool place = true;

    public void TogglePlaceOrErase()
    {
        place = !place;

        placeOrEraseText.text = place ? "Place" : "Erase";
    }

    public void Update()
    {
        if(OverUI())
            return;

        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(VoxelActionRoutine());
        }
    }

    private IEnumerator VoxelActionRoutine()
    {
        float touchTime = Time.time + 0.25f;

        while(Time.time < touchTime)
        {
            if(!Input.GetMouseButton(0))
            {
                print("Got up in time!");
                if(place)
                    PlaceVoxel();
                else
                    EraseVoxel();

                yield break;
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

    private void PlaceVoxel()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if(hit.collider.tag == "Building Surface")
                    CreateVoxel(hit.point + (hit.normal * 0.55f));
            }
        }
    }

    private void EraseVoxel()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if(hit.collider.tag == "Building Surface")
                    if(hit.collider.name == "Cube(Clone)")
                        Destroy(hit.collider.gameObject);
            }
        }
    }

    private void CreateVoxel(Vector3 position)
    {
        GameObject newVoxel = Instantiate(voxel, Vector3.zero, Quaternion.identity);

        newVoxel.transform.SetParent(GameObject.Find("Voxel Grid").transform);

        newVoxel.transform.localPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
    }
}
