using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class Tree : MonoBehaviour, IDamageable
{
    public static Tree Instance;

    public float StartingHealth;
    public float Health { get; private set; }

    public float Radius 
    {
        get { return collider?.radius ?? 0f; }
    }

    public GameObject BleedPrefab;

    private new CircleCollider2D collider;
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private List<Vector3> spriteStartingPositions = new List<Vector3>();

    private void Awake()
    {
        Instance = this;
        collider = GetComponentInChildren<CircleCollider2D>();
        sprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
        spriteStartingPositions.AddRange(sprites.ConvertAll(s => s.transform.localPosition));
    }

    private void Start()
    {
        Health = StartingHealth;
    }

    public void Damage(float damage)
    {
        for (int spriteIndex = 0; spriteIndex < sprites.Count; spriteIndex++)
        {
            Vector3 startingPosition = spriteStartingPositions[spriteIndex];
            SpriteRenderer sprite = sprites[spriteIndex];
            DOTween.Shake(() => sprite.transform.localPosition, v => sprite.transform.localPosition = startingPosition + v * 0.5f, 0.2f, Vector3.up * 0.1f);

            var point = collider.transform.position + Random.insideUnitSphere * collider.radius;
            Instantiate(BleedPrefab, point, Quaternion.identity);
        }

        Health = Mathf.Max(0, Health - damage);
        if (Health <= 0)
        {
            TreeHealthUI.Instance.GameOver();
        }
    }
}
