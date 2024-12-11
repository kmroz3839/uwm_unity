using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button buttonPlayDemo, buttonExit;

    // Start is called before the first frame update
    void Start()
    {
        buttonPlayDemo.onClick.AddListener(()=>{
            CrossScene.levelName = "tutorial";
            SceneManager.LoadScene("Level00");
        });
        buttonExit.onClick.AddListener(()=>{
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
