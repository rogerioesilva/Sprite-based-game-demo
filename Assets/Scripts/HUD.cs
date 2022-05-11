using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text Points;
    public Text Summary;
    public Slider PlayerEnergyLevel;

    private bool GamePlaying = true;
    private int points = 0;
    private int targetsHit = 0;

    private void Start()
    {
        PlayerEnergyLevel.maxValue = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnergyLevel;
        PlayerEnergyLevel.value = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnergyLevel;
        Summary.text = "";
    }

    public void AddPoints(int pts)
    {
        points += pts;
        targetsHit++;
    }

    private void OnGUI()
    {
        if(GamePlaying)
        {
            Points.text = points.ToString();
            PlayerEnergyLevel.value = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnergyLevel;
        }
        else
        {
            Points.text = "";
            Summary.text = "GAME OVER\n\nUFO: " + targetsHit.ToString() + "\nPoints: " + points.ToString();
        }
    }

    public void GameOver()
    {
        GamePlaying = false;
    }
}
