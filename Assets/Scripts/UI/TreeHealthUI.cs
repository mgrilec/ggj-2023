using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeHealthUI : MonoBehaviour
{
    public TextMeshProUGUI tree;
    public TextMeshProUGUI wave;

    public Color dangerColor;
    public Color normalColor;

    private void Update()
    {
        if (Tree.Instance)
        {
            tree.text = "roots: " + (Tree.Instance.Health / Tree.Instance.StartingHealth * 100f).ToString("0") + "%";
        }

        if (WaveManager.Instance.WaitingForNextWave)
        {
            wave.color = normalColor;
            wave.text = "next wave in: " + WaveManager.Instance.NextWaveInTime.ToString("0");
        }
        else
        {
            wave.color = dangerColor;
            wave.text = "DANGER!!";
        }
        
    }
}
