﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource death;


    // Start is called before the first frame update
  protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();




    }

    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        death.Play();
        rb.velocity = Vector2.zero;
        
        GetComponent<Collider2D>().enabled = false;
    }

    public void Death()
    {

        ScoreScript.scoreValue += 50;
        Destroy(this.gameObject);

    }

  
}
