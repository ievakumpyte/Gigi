using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    Rigidbody2D player;
    Rigidbody2D box;
    private float weight = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        box = GetComponent<Rigidbody2D>();
        box.mass = weight;
        box.gravityScale = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(player.position, box.position);

        StartCoroutine(StartRolling());
    }

    private IEnumerator StartRolling()
    {
        yield return new WaitForSeconds(10);
        box.constraints = RigidbodyConstraints2D.None;
        box.mass = 20;
        box.gravityScale = 6f;
    }


}
