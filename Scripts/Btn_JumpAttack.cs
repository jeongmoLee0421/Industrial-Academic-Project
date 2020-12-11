using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_JumpAttack : MonoBehaviour
{
    Button btn_JumpAttack;
    Button btn_Strike, btn_Attack;
    Player playerObject;
    private float playerTurnDelay = 1.5f;

    public void OnClickButton()
    {
        if (GameManager.instance.playerTurn == false || GameManager.instance.playerMP < 10) return;

        playerObject.JumpAttack();
        //GameManager.instance.playerTurn = false;
        StartCoroutine(PlayerTurnDelay());
    }

    IEnumerator PlayerTurnDelay()
    {
        if (btn_JumpAttack.IsInteractable() == true)
        {
            btn_Attack.interactable = false;
            btn_Strike.interactable = false;
            btn_JumpAttack.interactable = false;
        }
        yield return new WaitForSeconds(playerTurnDelay);
        GameManager.instance.playerTurn = false;
        if (btn_JumpAttack.IsInteractable() == false)
        {
            btn_Attack.interactable = true;
            btn_Strike.interactable = true;
            btn_JumpAttack.interactable = true;
        }
    }

    void Start()
    {
        btn_JumpAttack = GetComponent<Button>();
        btn_Attack = GameObject.FindWithTag("btn_Attack").GetComponent<Button>();
        btn_Strike = GameObject.FindWithTag("btn_Strike").GetComponent<Button>();
        playerObject = GameObject.FindWithTag("Player").GetComponent<Player>();
        btn_JumpAttack.onClick.AddListener(OnClickButton);
    }

    void Update()
    {
        
    }
}
