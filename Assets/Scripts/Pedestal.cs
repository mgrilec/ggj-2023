using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public string OffStateName;
    public string OnStateName;
    public string IdentStateName;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.Play(OffStateName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
