using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public string OffStateName;
    public string OnStateName;
    public string IdentStateName;
    public LayerMask PlayerLayerMask;
    public bool Opened;

    public Wave Wave;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.Play(OnStateName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger()
    {
        Opened = true;
        WaveManager.Instance.StartExtraWave(Wave);
        Debug.Log("Pedestal triggered!");
        animator.Play(IdentStateName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Opened && (PlayerLayerMask & 1 << collision.gameObject.layer) != 0)
        {
            Trigger();
        }
    }
}
