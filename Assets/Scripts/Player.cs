using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameManager manager;

    public int collected;

    private Rigidbody2D rb;

    public float speed;
    private Vector2 input;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isLocalPlayer) return;

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        manager.globalJacksText.text = "Global collected: " + manager.globalJacks;
        manager.jacksText.text = "Collected: " + collected;

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed / 100);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {   
        if (collider.gameObject.tag == "Jack")
        {
            Destroy(collider.gameObject);

            collected += 1;

            RpcGlobalJacks();
        }

    }

    [ClientRpc]
    private void RpcGlobalJacks()
    {
        manager.globalJacks += 1;
    }
}
