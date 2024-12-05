using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypartol : MonoBehaviour
{

    public GameObject A;

    public GameObject B;

    private Rigidbody2D rb;
    public SPUM_Prefabs anim;
    private float speed = 2.5f;

    private Transform currentpoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentpoint = A.transform;
        anim.PlayAnimation(1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentpoint.position - transform.position;
        if (currentpoint == B.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == B.transform)
        {
            Flip();
            currentpoint = A.transform;
        }
        if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f && currentpoint == A.transform)
        {
            Flip();
            currentpoint = B.transform;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the player by inverting the x scale
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(A.transform.position,0.5f);
        Gizmos.DrawWireSphere(B.transform.position,0.5f);
        Gizmos.DrawLine(A.transform.position, B.transform.position);
    }
}
