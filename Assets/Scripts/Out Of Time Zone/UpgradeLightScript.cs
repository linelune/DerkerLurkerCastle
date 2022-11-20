using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeLightScript : MonoBehaviour
{
    public Transform playerSoul;

    public Text text;
    public RawImage filter;

    public int level;

    public enum UPGRADE_TYPE { HEALTH, SPEED, JUMP };

    public int upgradeTypeIndex;

    private UPGRADE_TYPE upgradeType;

    private string healthUpgradeQuestion;
    private string speedUpgradeQuestion;
    private string jumpUpgradeQuestion;
    private string toAcceptText;
    private string giftTakenAnswer;
    private string notEnoughCoinAnswer;

    private int price;

    private DateTime timer;

    private bool isDisplayingAnswer;

    void Start()
    {
        upgradeType = (UPGRADE_TYPE) upgradeTypeIndex;
        healthUpgradeQuestion = "Do you want a gift of life from the goddess Aelia ?";
        speedUpgradeQuestion = "Do you want to be faster like the god Spror ?";
        jumpUpgradeQuestion = "Do you want to get close to the sky as the god Virleas ?";
        toAcceptText = "\nCome closer if that is what you want and if you possesses _ coins so that the god accept.";
        giftTakenAnswer = "Gift taken !";
        notEnoughCoinAnswer = "You need more coins if you want a new gift !";
        level = 0;
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
        }

        // Show filter depending on the distance between light and player

        if (Vector3.Distance(transform.position, playerSoul.position) < 7.0f)
        {
            if (upgradeType == UPGRADE_TYPE.HEALTH)
                filter.color = new Color(1.0f, 0.0f, 0.0f,
                    255.0f * ((1.0f - ((Vector3.Distance(transform.position, playerSoul.position) - 1.5f) / 5.5f)) / 4.0f) / 255.0f);
            else if (upgradeType == UPGRADE_TYPE.SPEED)
                filter.color = new Color(1.0f, 1.0f, 0.0f,
                    255.0f * ((1.0f - ((Vector3.Distance(transform.position, playerSoul.position) - 1.5f) / 5.5f)) / 4.0f) / 255.0f);
            else if (upgradeType == UPGRADE_TYPE.JUMP)
                filter.color = new Color(0.0f, 1.0f, 0.0f,
                    255.0f * ((1.0f - ((Vector3.Distance(transform.position, playerSoul.position) - 1.5f) / 5.5f)) / 4.0f) / 255.0f);
        }

        // Remove the question when the player go away from the light

        if ((upgradeType == UPGRADE_TYPE.HEALTH && text.text.StartsWith(healthUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.SPEED && text.text.StartsWith(speedUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.JUMP && text.text.StartsWith(jumpUpgradeQuestion))
            && Vector3.Distance(transform.position, playerSoul.position) > 7.0f)
        {
            text.text = "";

            filter.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }

        // Try to buy the upgrade when the player really close to the light

        // TO DO - Get player coins
        int playerCoins = 100;

        if ((upgradeType == UPGRADE_TYPE.HEALTH && text.text.StartsWith(healthUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.SPEED && text.text.StartsWith(speedUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.JUMP && text.text.StartsWith(jumpUpgradeQuestion))
            && Vector3.Distance(transform.position, playerSoul.position) < 1.5f
            && playerCoins > price)
        {
            playerCoins -= price;

            // TO DO - Upgrade player

            level += 1;
            updatePrice();

            playerSoul.position = new Vector3(0.0f, 1.0f, 0.0f);
            playerSoul.eulerAngles = Vector3.zero;

            text.text = giftTakenAnswer;
            timer = DateTime.Now;
            isDisplayingAnswer = true;

            filter.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else if ((upgradeType == UPGRADE_TYPE.HEALTH && text.text.StartsWith(healthUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.SPEED && text.text.StartsWith(speedUpgradeQuestion)
            || upgradeType == UPGRADE_TYPE.JUMP && text.text.StartsWith(jumpUpgradeQuestion))
            && Vector3.Distance(transform.position, playerSoul.position) < 1.5f)
        {
            playerSoul.position = new Vector3(0.0f, 1.0f, 0.0f);
            playerSoul.eulerAngles = Vector3.zero;

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
    }

    void updatePrice()
    {
        price = 10;

        for (int i = 0; i < level; i++)
        {
            price = (int) (price * 1.5f); 
        }
    }
}
