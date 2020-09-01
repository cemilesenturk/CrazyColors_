using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 15;

    public int minSwipe = 500;

    private bool yon;
    private Vector3 gıdecegıYon;

    private Vector2 swipePosLFrame;
    private Vector2 swipePosCFrame;
    private Vector2 cSwipe;

    private Vector3 nextCPosition;

    private Color sColor;

    private void Start()
    {
        sColor = Random.ColorHSV(.5f, 1);
        GetComponent<MeshRenderer>().material.color = sColor;
    }

    private void FixedUpdate()
    {

        if (yon)
        {
            rb.velocity = gıdecegıYon * speed;
        }


        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), .05f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Floor ground = hitColliders[i].transform.GetComponent<Floor>();

            if (ground && !ground.isColored)
            {
                ground.Colored(sColor);
            }

            i++;
        }


        if (nextCPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCPosition) < 1)
            {
                yon = false;
                gıdecegıYon = Vector3.zero;
                nextCPosition = Vector3.zero;
            }
        }

        if (yon)
            return;


        if (Input.GetMouseButton(0))
        {

            swipePosCFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLFrame != Vector2.zero)
            {


                cSwipe = swipePosCFrame - swipePosLFrame;

                if (cSwipe.sqrMagnitude < minSwipe)
                    return;

                cSwipe.Normalize();


                if (cSwipe.x > -0.5f && cSwipe.x < 0.5f)
                {
                    Hedef(cSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                if (cSwipe.y > -0.5f && cSwipe.y < 0.5f)
                {
                    Hedef(cSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }


            swipePosLFrame = swipePosCFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLFrame = Vector2.zero;
            cSwipe = Vector2.zero;
        }
    }

    private void Hedef(Vector3 direction)
    {
        gıdecegıYon = direction;


        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCPosition = hit.point;
        }

        yon = true;
    }
}
