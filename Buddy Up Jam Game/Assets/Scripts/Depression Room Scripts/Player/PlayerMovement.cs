using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    //TODO: Rename this variable tomorrow when I can think, this is not a good name for it
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        //I should not be programming this late and on this little sleep, what kind of solution is this?
        rb.AddForce(((transform.forward * (movement.z * moveSpeed)) + (transform.right * (movement.x * moveSpeed)))/(Mathf.Max((movement.x != 0 ? 1 : 0) + (movement.z != 0 ? 1 : 0), 1)));
    }
}
