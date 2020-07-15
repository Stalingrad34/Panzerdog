using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    private CharacterController _characterController = null;   
    private Animator _animator = null;
    private int layerMask = 0;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }


    private void Update()
    {
        float posX = Input.GetAxis("Horizontal");
        float posZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(posX, 0, posZ) * speed;
        movement = Vector3.ClampMagnitude(movement, speed) * Time.deltaTime;
        _characterController.Move(movement);

        //Look at mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            transform.LookAt(hit.point);
        }

        //Animation of movement
        _animator.SetFloat("MouseVertical", transform.TransformDirection(Vector3.forward).x);
        _animator.SetFloat("MouseHorizontal", transform.TransformDirection(Vector3.forward).z);
        _animator.SetFloat("KeyVertical", posZ);
        _animator.SetFloat("KeyHorizontal", posX);
    }
}
