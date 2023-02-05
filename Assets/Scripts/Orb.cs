using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tween;
    private TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> moveTween;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        tween = DOTween.To(() => transform.localScale, v => transform.localScale = v, Vector3.one * 1.1f, 0.2f);
        tween.SetLoops(-1, LoopType.Yoyo);

        moveTween = DOTween.To(() => transform.position, v => transform.position = v, Tree.Instance.transform.position, 5f);
        moveTween.OnComplete(() =>
        {

        });
    }

    private void OnDestroy()
    {
        DOTween.Kill(tween);
        DOTween.Kill(moveTween);
    }
}
