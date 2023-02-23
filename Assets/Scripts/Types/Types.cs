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
                return new Color(0.3f, 1f, 0.3f, 1f);
            case Type.Fire:
                return new Color(1f, 0.3f, 0.3f, 1f);
            case Type.Water:
                return new Color(0.3f, 0.3f, 1f, 1f);
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