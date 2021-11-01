using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton() {
        SceneManager.LoadScene("Bill Test Scene/Bill Test Scene");  //Currently not functioning   
    }

    public void ExitButton() {
        Application.Quit(); //Cannot be tested in editor (???)
    }
}
