using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptsButton : MonoBehaviour
{
    public void Next()
    {
    	
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Next2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Back2()
    {
    	
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void Accueil()
    {
    	
        SceneManager.LoadScene(0);
    }

    public void Game()
    {
        
        SceneManager.LoadScene(1);
    }

    public void Menu(){
        
        SceneManager.LoadScene(2);
    }


    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
