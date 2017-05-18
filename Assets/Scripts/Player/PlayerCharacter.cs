using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomFunctions;

public class PlayerCharacter : Character {

    // Controls
    private float score;
    private float money;

    // Components
    public Image healthBar;
    public Text scoreText;
    public Text moneyText;
    public AudioClip damageSound;
    public Image damageSplash;

    void Start ()
    {
        damageSplash.enabled = false; // Make sure the splash screen is disabled on start
    }

    public override void Damage(float amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        base.Damage(amount, hitPoint, hitDirection);
        healthBar.fillAmount = health / 100; // Update healthbar UI
        SoundManager.Main.Play(damageSound);
        StartCoroutine(DeathSplash()); // Flash the screen red
    }

    // Called when the players health is 0
    public override void Die()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single); // Load the EndGame scene
    }

    //Increase the players score
	public void AddScore(float amount)
    {
        score += amount;
        SetText(scoreText, score);
    }

    // Increase the players money
    public void AddMoney(float amount)
    {
        money += amount;
        SetText(moneyText, money);
    }

    // Increase the players health
    public void AddHealth(float amount)
    {
        health += amount;
        health = Maths.Clamp(health, 0, 100); // Limit health to 100 percent
        healthBar.fillAmount = health / 100; // Update healthbar UI
    }

    // Used for setting the text for the HUD elements text such as health display
    private void SetText(Text textObject, float amount)
    {
        textObject.text = string.Format(textObject.name + " : {0}", amount);
    }

    IEnumerator DeathSplash()
    {
        damageSplash.enabled = true;
        yield return new WaitForSeconds(.1f);
        damageSplash.enabled = false;
        yield break;
    }
}
