using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine;

public class FileViewer : MonoBehaviour
{
    [SerializeField] private FileViewObject fileViewObjectPrefab;
    private string[] ovms;
    [SerializeField] private int page = 1;
    [SerializeField] private int filesPerPage;

    public void Start()
    {
        Refresh();
        ValidatePages();
        RenderFileView();
    }

    public void Refresh()
    {
        ovms = GetVoxelModelNames();
    }

    private string[] GetVoxelModelNames()
    {
        string[] pathArray = FileManagement.GetPathArrayOfOVMs();

        print(pathArray);

        Regex nameFindingRegex = new Regex(@"(?!.*/)(.*\.ovm$)", RegexOptions.Compiled);

        List<string> modelNames = new List<string>();

        foreach(string path in pathArray)
        {
            string file = nameFindingRegex.Matches(path)[0].Value;

            modelNames.Add(file.Substring(0, file.Length-4));
        }

        return modelNames.ToArray();
    }

    public void RenderFileView()
    {
        ClearFileView();

        int limit = page * filesPerPage > ovms.Length ? ovms.Length - ((page-1) * filesPerPage) : filesPerPage;

        for(int i = 0; i < limit; i++)
        {
            CreateFileViewObject(i);
        }
    }

    private void ClearFileView()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateFileViewObject(int index)
    {
        RectTransform newFileViewObject = 
            Instantiate(fileViewObjectPrefab, Vector3.zero, Quaternion.identity).GetComponent<RectTransform>();

        float elementHeight = 1f/filesPerPage;

        newFileViewObject.transform.SetParent(transform, false);

        newFileViewObject.anchorMin = new Vector2(0f, 1 - ((index+1) * elementHeight));
        newFileViewObject.anchorMax = new Vector2(1f, 1 - (index * elementHeight));

        if(index % 2 == 0)
        {
            newFileViewObject.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }

        newFileViewObject.GetComponent<FileViewObject>().fileName = ovms[index + ((page-1) * filesPerPage)];
    }

    public void NextPage()
    {
        if(!AtEnd())
            page++;

        ValidatePages();

        RenderFileView();
    }

    public void LastPage()
    {
        if(!AtBeginning())
            page--;

        ValidatePages();

        RenderFileView();
    }

    private void ValidatePages()
    {
        Button lastPage = GameObject.Find("Last Page").GetComponent<Button>();
        Button nextPage = GameObject.Find("Next Page").GetComponent<Button>();

        lastPage.interactable = AtBeginning() ? false : true;
        nextPage.interactable = AtEnd() ? false : true;

        Text lpText = lastPage.transform.GetChild(0).GetComponent<Text>();
        Text npText = nextPage.transform.GetChild(0).GetComponent<Text>();

        if(lastPage.interactable)
            lpText.text = "Page "+(page-1);
        else
            lpText.text = "";

        if(nextPage.interactable)
            npText.text = "Page "+(page+1);
        else
            npText.text = "";
    }

    private bool AtEnd()
    {
        return (page) * filesPerPage > ovms.Length;
    }

    private bool AtBeginning()
    {
        return page == 1;
    }
}