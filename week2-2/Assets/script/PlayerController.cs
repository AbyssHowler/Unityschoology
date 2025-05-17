using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animation damageEffect;
    public Text scoreText;
    public Text hpText;

    int HP;
    int score;


    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = 50;
        score = 0;
        scoreText.text = "Score: " + score;
        hpText.text = "HP: " + HP;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyDamage(int damage)
    {
        damageEffect.Play();
        HP -= damage;
        hpText.text = "HP: " + HP;
        if (HP <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void ScoreUp(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + this.score;
    }
}
