using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private BinaryFormatter binaryFormatter = null;
    private UpgradeManager upgradeManager = null;

    private readonly string saveFileName = "/SaveV2.dat";

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (binaryFormatter == null || upgradeManager == null)
            InitValue();
    }

    public void SaveData()
    {
        if (binaryFormatter == null || upgradeManager == null)
            InitValue();

        FileStream file = File.Create(
            Application.persistentDataPath + saveFileName
        );
        Save save = new Save();

        save.savedPlayerSpeed = upgradeManager.playerSpeed;
        save.savedPlayerMaxHealth = upgradeManager.playerHealth;
        save.savedPlayerJumpHeight = upgradeManager.playerJumpHeight;
        save.savedPlayerDamageMultiplier = upgradeManager.playerDamage;
        save.savedPlayerLevel = upgradeManager.playerLevel;
        save.savedPlayerCoins = upgradeManager.coins;

        binaryFormatter.Serialize(file, save);

        file.Close();
        
        Debug.Log("Data Saved !");
    }

    public void LoadData()
    {
        if (binaryFormatter == null || upgradeManager == null)
            InitValue();

        if (File.Exists(Application.persistentDataPath + saveFileName))
        {
            FileStream file = File.Open(
                Application.persistentDataPath + saveFileName, FileMode.Open
            );
            Save save = (Save) binaryFormatter.Deserialize(file);

            file.Close();
            
            upgradeManager.playerSpeed = save.savedPlayerSpeed;
            upgradeManager.playerHealth = save.savedPlayerMaxHealth;
            upgradeManager.playerJumpHeight = save.savedPlayerJumpHeight;
            upgradeManager.playerDamage = save.savedPlayerDamageMultiplier;
            upgradeManager.playerLevel = save.savedPlayerLevel;
            upgradeManager.coins = save.savedPlayerCoins;

            Debug.Log("Data loaded !");
        }
    }

    public void ResetData()
    {
        if (binaryFormatter == null || upgradeManager == null)
            InitValue();

        if (File.Exists(Application.persistentDataPath + saveFileName))
        {
            File.Delete(
                Application.persistentDataPath + saveFileName
            );

            Debug.Log("Data reset !");
        }
    }

    private void InitValue()
    {
        binaryFormatter = new BinaryFormatter();
        upgradeManager = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }
}