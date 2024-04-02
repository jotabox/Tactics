using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public delegate void  DelegateModel(object sender, object args);
public class InputController : MonoBehaviour
{

    private  InputActions inputAction;
    public static InputController instance;
    public DelegateModel OnMove;
    public DelegateModel OnFire;

    private void Awake()
    {
        instance = this;
        inputAction = new InputActions();
        inputAction.Enable();
    }

   
    private void Update()
    {
        Vector2 moveInput = inputAction.Game.Move.ReadValue<Vector2>();
        if(moveInput != Vector2.zero && OnMove != null)
        {
            OnMove(null, moveInput);
        }


        if (inputAction.Game.Select.WasPressedThisFrame() && OnFire != null)
        {
           var button = inputAction.Game.Select.WasReleasedThisFrame();
            OnFire(null, button);
        }
    }


}
