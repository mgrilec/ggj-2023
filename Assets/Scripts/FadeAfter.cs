using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAfter : MonoBehaviour
{
    public float Delay = 5f;
    private float spawnTime;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime > Delay)
        {
            sprite.color = Color.Lerp(sprite.color, new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f), Time.deltaTime);
            if (sprite.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
