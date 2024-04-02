using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;



public delegate void  DelegateModel(object sender, object args);
public class InputController : MonoBehaviour
{

    private  InputActions inputAction;
    public static InputController instance;
    public DelegateModel OnMove;
    public DelegateModel OnFire;

    float hCooldown = 0;
    float vCooldown = 0;
    float cooldownTimer = 0.5f;

    private void Awake()
    {
        instance = this;
        inputAction = new InputActions();
        inputAction.Enable();
    }

   
    private void Update()
    {
        Vector3 moveInput = inputAction.Game.Move.ReadValue<Vector3>();

        int horizon = Mathf.RoundToInt(moveInput.y);
        int vertic = Mathf.RoundToInt(moveInput.x);

        Vector3Int moved = new Vector3Int(0, 0, 0);

        if (horizon !=0)
        {
            moved.x = GetMove(ref hCooldown, horizon);
        }
        else
        {
            hCooldown = 0;
        }

        if (vertic !=0)
        {
            moved.y = GetMove(ref vCooldown, vertic);
        }
        else
        {
            vCooldown = 0;
        }

        if(moved != Vector3.zero && OnMove != null)
        {
            OnMove(null, moved);
        }


        if (inputAction.Game.Select.WasPressedThisFrame() && OnFire != null)
        {
           var button = inputAction.Game.Select.WasReleasedThisFrame();
            OnFire(null, button);
        }
    }


    int GetMove(ref float cooldownSum, int value)
    {
        if(Time.time > cooldownSum)
        {
            cooldownSum += Time.time + cooldownTimer;
            return value;
        }
        return 0;
    }


}
