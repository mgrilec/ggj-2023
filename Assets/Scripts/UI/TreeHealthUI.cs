using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeHealthUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!Tree.Instance)
        {
            return;
        }

        text.text = (Tree.Instance.Health / Tree.Instance.StartingHealth * 100f).ToString("0") + "%";
    }
}
