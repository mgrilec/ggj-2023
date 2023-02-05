using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TreeHealthUI : MonoBehaviour
{
    public static TreeHealthUI Instance;

    public TextMeshProUGUI tree;
    public TextMeshProUGUI wave;
    public Transform GameOverParent;
    public Image GameOverImage;
    public TextMeshProUGUI orbsText;
    public Transform VictoryParent;
    public Image VictoryImage;

    public Color dangerColor;
    public Color normalColor;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Tree.Instance)
        {
            tree.text = "roots: " + (Tree.Instance.Health / Tree.Instance.StartingHealth * 100f).ToString("0") + "%";
            orbsText.text = $"{Tree.Instance.Orbs}/3";
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    public void GameOver()
    {
        GameOverParent.gameObject.SetActive(true);
        DOTween.To(() => GameOverImage.color, v => GameOverImage.color = v, new Color(GameOverImage.color.r, GameOverImage.color.g, GameOverImage.color.b, 1f), 2f);
    }

    public void Victory()
    {
        VictoryParent.gameObject.SetActive(true);
        DOTween.To(() => VictoryImage.color, v => VictoryImage.color = v, new Color(VictoryImage.color.r, VictoryImage.color.g, VictoryImage.color.b, 1f), 2f);
    }
}
