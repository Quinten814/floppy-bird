using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using UnityEngine.SceneManagement;
public class DefaultScript : MonoBehaviour
{
    public bool changed = false;
    public GameObject dfault;
    public GameObject checkmark;
    string skin;
    public string skinname;
    public GameObject firstskin;
    public GameObject secondskin;
    public GameObject thirdskin;
    async void rrr()
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "skin" });
        skin = savedData["skin"];
    }
    // Start is called before the first frame update
    void Start()
    {
        rrr();
    }

    // Update is called once per frame
    void Update()
    {
        if (skinname == skin)
        {
            checkmark.SetActive(true);
        }
    }
    public async void SaveSomeData()
    {
        var data = new Dictionary<string, object> { { "skin", skinname } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        rrr();
        changed = true;
        checkmark.SetActive(true);
        firstskin.transform.Find("checkmark").gameObject.SetActive(false);
        secondskin.transform.Find("checkmark").gameObject.SetActive(false);
        thirdskin.transform.Find("checkmark").gameObject.SetActive(false);
        firstskin.GetComponent<DefaultScript>().skin = "sono chi no sadame jooooooooooooooooooooooo";
        secondskin.GetComponent<DefaultScript>().skin = "nice nice very nice ceasar-chan";
        thirdskin.GetComponent<DefaultScript>().skin = "go ahead miiiiistaaaaaaaa";
    }
    public void goback()
    {
        SceneManager.LoadScene(1);
    }
}
