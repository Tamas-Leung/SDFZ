using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{

    private Label label;
    private int currentHealth;
    private int maxHealth;
    private VisualElement[] heartContainers;

    void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        // label = rootVisualElement.Q<Label>(name: "Health");
        heartContainers = rootVisualElement.Q<GroupBox>("HeartGroupBox").Children().ToArray();
    }

    public void Init(Player player)
    {
        player.OnCurrentHealthChange += CurrentHealthChange;
        player.OnCurrentMaxHealthChange += MaxHealthChange;
        UpdateHealthVisual();
    }

    void CurrentHealthChange(int newVal)
    {
        currentHealth = newVal;
        UpdateHealthVisual();
    }

    void MaxHealthChange(int newVal)
    {
        maxHealth = newVal;
        UpdateHealthVisual();
    }

    void UpdateHealthVisual()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            IMGUIContainer heartContainer = heartContainers[i] as IMGUIContainer;
            if (i + 1 <= currentHealth)
            {
                heartContainer.style.unityBackgroundImageTintColor = Color.white;
            }
            else if (i + 1 <= maxHealth)
            {
                heartContainer.style.unityBackgroundImageTintColor = Color.black;
            }
            else
            {
                heartContainer.style.unityBackgroundImageTintColor = Color.clear;
            }
        }
    }
}
