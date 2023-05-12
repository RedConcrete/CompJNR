using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMaker : MonoBehaviour
{
    

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
