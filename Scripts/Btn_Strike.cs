using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Strike : MonoBehaviour
{
    Button btn_Strike;
    Button btn_Attack, btn_JumpAttack;
    Player playerObject;
    private float playerTurnDelay = 1.5f;

    public void OnClickButton()
    {
        if (GameManager.instance.playerTurn == false || GameManager.instance.playerMP < 20) return;

        playerObject.Strike();
        //GameManager.instance.playerTurn = false;
        StartCoroutine(PlayerTurnDelay());
    }

    IEnumerator PlayerTurnDelay()
    {
        if (btn_Strike.IsInteractable() == true)
        {
            btn_Attack.interactable = false;
            btn_Strike.interactable = false;
            btn_JumpAttack.interactable = false;
        }
        yield return new WaitForSeconds(playerTurnDelay);
        GameManager.instance.playerTurn = false;
        if (btn_Strike.IsInteractable() == false)
        {
            btn_Attack.interactable = true;
            btn_Strike.interactable = true;
            btn_JumpAttack.interactable = true;
        }
    }

    void Start()
    {
        btn_Strike = GetComponent<Button>();
        btn_Attack = GameObject.FindWithTag("btn_Attack").GetComponent<Button>();
        btn_JumpAttack = GameObject.FindWithTag("btn_JumpAttack").GetComponent<Button>();
        playerObject = GameObject.FindWithTag("Player").GetComponent<Player>();
        btn_Strike.onClick.AddListener(OnClickButton);
    }

    void Update()
    {
        
    }
}
