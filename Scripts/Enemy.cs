using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [HideInInspector] public int enemyHP;
    [HideInInspector] public int enemyPower;
    [HideInInspector] public int enemyArmor;

    private Animator animator;
    private float dieDelay = 1f;
    private Player playerObject;

    public float height = 1.7f;
    RectTransform hpBar;
    public Image nowHPbar;
    public Text enemyHP_Text;
    public Vector3 _hpBarPos;
    public GameObject hudDamageText;
    public GameObject canvas;

    void Start()
    {
        enemyHP = GameManager.instance.enemyHP;
        enemyPower = GameManager.instance.enemyPower;
        enemyArmor = GameManager.instance.enemyArmor;
        animator = GetComponent<Animator>();
        playerObject = GameObject.FindWithTag("Player").GetComponent<Player>();

        hpBar = GameObject.Find("enemyBGHP_BAR").GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas1");
        nowHPbar = GameObject.Find("enemyBGHP_BAR").transform.Find("enemyHP_BAR").GetComponent<Image>();
        enemyHP_Text = GameObject.Find("enemyBGHP_BAR").transform.Find("enemyHP_Text").GetComponent<Text>();
        _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - 1.2f, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;

    }

    void Update()
    {
        nowHPbar.fillAmount = (float)enemyHP / GameManager.instance.enemyHP;
        enemyHP_Text.text = string.Format("{0}/" + GameManager.instance.enemyHP.ToString(), enemyHP);
        if (GameManager.instance.playerTurn == true) return;
        Attack();
    }

    public void Attack()
    {
        int damage = enemyPower * 1;
        //GameObject.FindWithTag("Player").GetComponent<Player>().Hit(damage);
        playerObject.Hit(damage);
        animator.SetTrigger("EnemyAttack");
        //animator.SetTrigger("Enemy1Attack");
        //animator.SetTrigger("Enemy" + ((GameManager.instance.level)).ToString() + "Attack");
        Debug.Log("EnemyAttack!");
        GameManager.instance.playerTurn = true;
    }

    public void Hit(int damage)
    {
        int decreaseDamage = damage - enemyArmor;
        if (decreaseDamage <= 0) decreaseDamage = 1;
        enemyHP -= decreaseDamage;
        GameObject hudText = Instantiate(hudDamageText, canvas.transform);
        hudText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x-2.2f, transform.position.y + height+1.0f, 0));
        hudText.GetComponent<dmg_txt>().dmg = decreaseDamage;
        //if (enemyHP <= 0) Die();
        if (enemyHP <= 0)
        {
            enemyHP = 0;
            StartCoroutine(WaitDie());
        }
    }

    IEnumerator WaitDie()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(dieDelay);
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
        //GameManager.instance.GameOver();

        if (GameManager.stage == 1)
        {
            if (GameManager.instance.level % 4 != 0)
                SceneManager.LoadScene("Tutorial_Map");
            else if (GameManager.instance.level % 4 == 0)
            {
                SceneManager.LoadScene("Main_display");
                GameManager.stage = 2;
            }
        }
        else if (GameManager.stage == 2)
        {
            if (GameManager.instance.level < 7 )
                SceneManager.LoadScene("Forest_Map");
            else if (GameManager.instance.level == 7)
            {
                SceneManager.LoadScene("Main_display");
                GameManager.stage = 3;
            }
        }
        else if (GameManager.stage == 3)
        {
            if (GameManager.instance.level < 10)
                SceneManager.LoadScene("Cave_Map");
            else if (GameManager.instance.level == 10)
            {
                SceneManager.LoadScene("Main_display");
                GameManager.stage = 4;
            }
        }
    }
}
