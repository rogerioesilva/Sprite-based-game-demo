using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public List<string> Scenes = new List<string>();
    public List<Image> Logos = new List<Image>();
    public int selectedLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnGUI()
    {
        for(int img=0; img<Logos.Count; img++)
        {
            Logos[img].enabled = img == selectedLevel;
        }
    }

    public void AppQuit()
    {
        Application.Quit();
    }

    public void LoadSelectedLevel()
    {
        SceneManager.LoadScene(Scenes[selectedLevel]);
    }
}
