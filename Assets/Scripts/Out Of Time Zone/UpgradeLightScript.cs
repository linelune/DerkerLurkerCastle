using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UpgradeLightScript : MonoBehaviour
{
    public Transform playerSoul;
    public CharacterController playerSoulCharacterController;

    public Text text;
    public RawImage filter;

    public int level;

    public enum UPGRADE_TYPE { HEALTH, SPEED, JUMP, DAMAGE };

    public int upgradeTypeIndex;

    private UPGRADE_TYPE upgradeType;

    private string healthUpgradeQuestion;
    private string speedUpgradeQuestion;
    private string jumpUpgradeQuestion;
    private string damageUpgradeQuestion;
    private string toAcceptText;
    private string giftTakenAnswer;
    private string notEnoughCoinAnswer;

    private int price;

    private DateTime timer;

    private bool isDisplayingAnswer;

    private UpgradeManager um;

    void Start()
    {
        um = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        upgradeType = (UPGRADE_TYPE) upgradeTypeIndex;
        
        healthUpgradeQuestion = "Do you want a gift of life from the goddess Aelia ?";
        speedUpgradeQuestion = "Do you want to be faster like the god Spror ?";
        jumpUpgradeQuestion = "Do you want to get close to the sky as the god Virleas ?";
        damageUpgradeQuestion = "Do you want to be as strong as the god Minaor ?";

        toAcceptText = "\nCome closer if that is what you want and if you possesses _ coins so that the god accept.";
        giftTakenAnswer = "Gift taken !";
        notEnoughCoinAnswer = "You need more coins if you want a new gift !";
        level = um.playerLevel;
        updatePrice();
        timer = DateTime.Now;
        isDisplayingAnswer = false;
    }

    void Update()
    {
        // Show question when the player get close to the light

        if (Vector3.Distance(transform.position, playerSoul.position) < 7.0f && text.text == "")
        {
            string toAcceptTextWithPrice = toAcceptText.Replace("_", price.ToString());

            if (upgradeType == UPGRADE_TYPE.HEALTH)
                text.text = healthUpgradeQuestion + toAcceptTextWithPrice;
            else if (upgradeType == UPGRADE_TYPE.SPEED)
                text.text = speedUpgradeQuestion + toAcceptTextWithPrice;
            else if (upgradeType == UPGRADE_TYPE.JUMP)
                text.text = jumpUpgradeQuestion + toAcceptTextWithPrice;
            else if (upgradeType == UPGRADE_TYPE.DAMAGE)
                text.text = damageUpgradeQuestion + toAcceptTextWithPrice;
        }

        // Show filter depending on the distance between light and player

        if (Vector3.Distance(transform.position, playerSoul.position) < 7.0f)
        {
            float alpha = 255.0f * ((1.0f - ((Vector3.Distance(transform.position, playerSoul.position) - 1.5f) / 5.5f)) / 4.0f) / 255.0f;
            if (upgradeType == UPGRADE_TYPE.HEALTH)
                filter.color = new Color(1.0f, 0.0f, 0.0f, alpha);
            else if (upgradeType == UPGRADE_TYPE.SPEED)
                filter.color = new Color(1.0f, 1.0f, 0.0f, alpha);
            else if (upgradeType == UPGRADE_TYPE.JUMP)
                filter.color = new Color(0.0f, 1.0f, 0.0f, alpha);
            else if (upgradeType == UPGRADE_TYPE.DAMAGE)
                filter.color = new Color(1.0f, 0.0f, 1.0f, alpha);
        }

        // Remove the question when the player go away from the light

        if ((upgradeType == UPGRADE_TYPE.HEALTH && text.text.StartsWith(healthUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.SPEED && text.text.StartsWith(speedUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.JUMP && text.text.StartsWith(jumpUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.DAMAGE && text.text.StartsWith(damageUpgradeQuestion))
            && Vector3.Distance(transform.position, playerSoul.position) > 7.0f)
        {
            text.text = "";

            filter.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }

        // Try to buy the upgrade when the player really close to the light

        if ((upgradeType == UPGRADE_TYPE.HEALTH && text.text.StartsWith(healthUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.SPEED && text.text.StartsWith(speedUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.JUMP && text.text.StartsWith(jumpUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.DAMAGE && text.text.StartsWith(damageUpgradeQuestion))
            && Vector3.Distance(transform.position, playerSoul.position) < 1.5f
            && um.coins > price)
        {
            um.coins -= price;

            UIManager.coinDisplay.text = um.coins.ToString();

            switch (upgradeType){
                case UPGRADE_TYPE.HEALTH:
                        GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>().UpgradeHealth();
                    break; 
                case UPGRADE_TYPE.SPEED:
                        GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>().UpgradeSpeed();
                    break;
                case UPGRADE_TYPE.JUMP:
                        GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>().UpgradeJump();
                    break;
                case UPGRADE_TYPE.DAMAGE:
                        GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>().UpgradeDamage();
                    break;
            }

            um.playerLevel += 1;

            playerSoulCharacterController.enabled = false;

            playerSoul.position = new Vector3(0.0f, 1.0f, 0.0f);
            playerSoul.eulerAngles = Vector3.zero;

            playerSoulCharacterController.enabled = true;

            text.text = giftTakenAnswer;
            timer = DateTime.Now;
            isDisplayingAnswer = true;

            filter.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else if ((upgradeType == UPGRADE_TYPE.HEALTH && text.text.StartsWith(healthUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.SPEED && text.text.StartsWith(speedUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.JUMP && text.text.StartsWith(jumpUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.DAMAGE && text.text.StartsWith(damageUpgradeQuestion))
            && Vector3.Distance(transform.position, playerSoul.position) < 1.5f)
        {
            playerSoulCharacterController.enabled = false;

            playerSoul.position = new Vector3(0.0f, 1.0f, 0.0f);
            playerSoul.eulerAngles = Vector3.zero;

            playerSoulCharacterController.enabled = true;

            text.text = notEnoughCoinAnswer;
            timer = DateTime.Now;
            isDisplayingAnswer = true;

            filter.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }

        // Remove answer text

        TimeSpan timePassed = DateTime.Now - timer;

        if (timePassed.TotalSeconds > 2 && isDisplayingAnswer && (text.text == giftTakenAnswer || text.text == notEnoughCoinAnswer))
        {
            text.text = "";
            isDisplayingAnswer = false;
        }

        updatePrice();
    }

    void updatePrice()
    {
        price = (int)(10 * 1.5 * um.playerLevel);
    }
}
