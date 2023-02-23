using System;
using System.Collections.Generic;

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
        new PowerUp(UpgradeOption.DecreaseAttackCooldown, 0.2f, UpgradeTier.Epic),
        new PowerUp(UpgradeOption.DecreaseAttackCooldown, 0.1f, UpgradeTier.Rare),
        new PowerUp(UpgradeOption.DecreaseAttackCooldown, 0.05f, UpgradeTier.Common),
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
                return $"Decrease Attack Cooldown by {powerUp.value}";
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
        List<int> randomIndexes = new List<int>();
        Random rand = new Random();
        while (randomIndexes.Count < 3)
        {
            int index = rand.Next(0, powerUps.Length);
            if (!randomIndexes.Contains(index))
            {
                randomIndexes.Add(index);
            }
        }

        return randomIndexes.ConvertAll<PowerUp>(index => powerUps[index]).ToArray();
    }
}