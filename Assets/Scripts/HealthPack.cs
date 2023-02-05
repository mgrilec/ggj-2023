using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float HealAmount;
    public LayerMask PickupLayerMask;

    private SpriteRenderer sprite;
    private TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tween;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tween = DOTween.To(() => sprite.transform.localPosition, v => sprite.transform.localPosition = v, Vector3.up * 0.2f, 1f);
        tween.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((PickupLayerMask & 1 << collision.attachedRigidbody.gameObject.layer) != 0)
        {
            var player = collision.attachedRigidbody.GetComponent<Player>();
            player.Heal(HealAmount);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(tween);
    }
}
