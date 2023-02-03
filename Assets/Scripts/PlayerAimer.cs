using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimer : MonoBehaviour
{
    public float DeadZone;

    SpriteRenderer sprite;

    private Player player;
    private Color startingColor;
    private Color color;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        startingColor = sprite.color;

        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        var altHorizontal = Input.GetAxis($"alt-horizontal-{player.PlayerIndex}");
        var altVertical = Input.GetAxis($"alt-vertical-{player.PlayerIndex}");
        if (Mathf.Abs(altHorizontal) < DeadZone && Mathf.Abs(altVertical) < DeadZone)
        {
            sprite.color = Color.Lerp(sprite.color, new Color(startingColor.r, startingColor.g, startingColor.b, 0f), 10f * Time.deltaTime);
        }
        else
        {
            sprite.color = Color.Lerp(sprite.color, startingColor, 10f * Time.deltaTime);
            float altAngle = Mathf.Atan2(altVertical, altHorizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(altAngle, Vector3.forward);
        }
    }
}
