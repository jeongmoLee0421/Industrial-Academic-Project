using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Progress : MonoBehaviour
{
    Button button;
    Enemy enemyObject;

    public void OnClickButton()
    {
        if (GameManager.instance.playerTurn == true) return;

        enemyObject.Attack();
        GameManager.instance.playerTurn = true;
    }

    void Start()
    {
        button = GetComponent<Button>();
        enemyObject = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        button.onClick.AddListener(OnClickButton);
    }

    void Update()
    {
        
    }
}
