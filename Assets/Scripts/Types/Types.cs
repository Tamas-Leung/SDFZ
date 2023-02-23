using UnityEngine;

public enum Type
{
    Wood,
    Fire,
    Water,
    None
}


static class TypeMethods
{
    public static Color GetColorFromType(Type type)
    {
        switch (type)
        {
            case Type.Wood:
                return Color.green;
            case Type.Fire:
                return Color.red;
            case Type.Water:
                return Color.blue;
            default:
                return Color.white;
        }
    }

    public static string GetNameFromType(Type type)
    {
        switch (type)
        {
            case Type.Wood:
                return "Wood";
            case Type.Fire:
                return "Fire";
            case Type.Water:
                return "Water";
            default:
                return "None";
        }
    }

    public static Type GetDisavantageType(Type type)
    {
        switch (type)
        {
            case Type.Wood:
                return Type.Fire;
            case Type.Fire:
                return Type.Water;
            case Type.Water:
                return Type.Wood;
            default:
                return Type.None;
        }
    }
}