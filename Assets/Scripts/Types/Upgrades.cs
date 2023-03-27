using System;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeOption
{
    IncreaseMaxHealth,
    IncreaseAttackPower,
    IncreaseMoveSpeed,
    DecreaseAttackCooldown,
    IncreaseDashRange,
    DecreaseDashCooldown,
    AddForm
}



public enum UpgradeTier
{
    Common,
    Rare,
    Epic
}

public class PowerUp
{
    public UpgradeOption upgrade;

    //float, int or Type depending on upgrade
    public object value;
    public UpgradeTier tier;

    public PowerUp(UpgradeOption upgrade, object value, UpgradeTier tier)
    {
        this.upgrade = upgrade;
        this.value = value;
        this.tier = tier;
    }
}

public class PowerUpMethods
{
    public static PowerUp[] powerUps = {
        new PowerUp(UpgradeOption.IncreaseMaxHealth, 3, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.IncreaseMaxHealth, 2, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.IncreaseMaxHealth, 1, UpgradeTier.Common),
        new PowerUp(UpgradeOption.IncreaseAttackPower, 4f, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.IncreaseAttackPower, 2f, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.IncreaseAttackPower, 1f, UpgradeTier.Common),
        new PowerUp(UpgradeOption.IncreaseMoveSpeed, 0.4f, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.IncreaseMoveSpeed, 0.2f, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.IncreaseMoveSpeed, 0.1f, UpgradeTier.Common),
        new PowerUp(UpgradeOption.DecreaseAttackCooldown, 0.05f, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.DecreaseAttackCooldown, 0.02f, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.DecreaseAttackCooldown, 0.01f, UpgradeTier.Common),
        new PowerUp(UpgradeOption.IncreaseDashRange, 0.5f, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.IncreaseDashRange, 0.2f, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.IncreaseDashRange, 0.1f, UpgradeTier.Common),
        new PowerUp(UpgradeOption.DecreaseDashCooldown, 0.4f, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.DecreaseDashCooldown, 0.2f, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.DecreaseDashCooldown, 0.1f, UpgradeTier.Common),
    };

    public static string GetPowerUpString(PowerUp powerUp)
    {
        switch (powerUp.upgrade)
        {
            case UpgradeOption.IncreaseMaxHealth:
                return $"Increase Health by {powerUp.value}";
            case UpgradeOption.IncreaseAttackPower:
                return $"Increase Attack Power by {powerUp.value}";
            case UpgradeOption.IncreaseMoveSpeed:
                return $"Increase Move speed by {powerUp.value}";
            case UpgradeOption.DecreaseAttackCooldown:
                return $"Decrease Attack Cooldown by {((float)powerUp.value) * 100}%";
            case UpgradeOption.IncreaseDashRange:
                return $"Increase Dash Range by {powerUp.value}";
            case UpgradeOption.DecreaseDashCooldown:
                return $"Decrease Dash Cooldown by {powerUp.value}";
            case UpgradeOption.AddForm:
                return $"Gain the powers of {TypeMethods.GetNameFromType((Type)powerUp.value)}";
            default:
                return "";
        }
    }

    public static PowerUp[] GetThreeRandomPowerUps()
    {
        List<PowerUp> randomPowerUps = new List<PowerUp>();

        int totalWeight = 0;
        foreach (PowerUp powerUp in powerUps)
        {
            totalWeight += getPowerUpWeight(powerUp);
        }

        System.Random rand = new System.Random();
        while (randomPowerUps.Count < 3)
        {
            int randomWeight = rand.Next(0, totalWeight);

            int currentWeight = 0;
            foreach (PowerUp powerUp in powerUps)
            {
                currentWeight += getPowerUpWeight(powerUp);
                if (randomWeight <= currentWeight)
                {
                    randomPowerUps.Add(powerUp);
                    break;
                }
            }
        }

        return randomPowerUps.ToArray();
    }

    public static Color GetColorFromRarity(PowerUp powerUp)
    {
        switch (powerUp.tier)
        {
            case UpgradeTier.Common:
                return new Color(0.86f, 0.86f, 0.86f);
            case UpgradeTier.Epic:
                return new Color(0.54f, 0.17f, 0.89f);
            case UpgradeTier.Rare:
                return new Color(0.12f, 0.56f, 1f);
            default:
                return Color.white;
        }
    }

    private static int getPowerUpWeight(PowerUp powerUp)
    {
        switch (powerUp.tier)
        {
            case UpgradeTier.Common:
                return 6;
            case UpgradeTier.Epic:
                return 1;
            case UpgradeTier.Rare:
                return 3;
            default:
                return 6;
        }
    }
}