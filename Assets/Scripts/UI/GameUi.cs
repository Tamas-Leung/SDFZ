using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUi : MonoBehaviour
{

    private Label label;
    private int currentHealth;
    private int maxHealth;
    private VisualElement[] heartContainers;
    private VisualElement formsEarnedBox;
    public VisualTreeAsset formEarnedTextBox;
    private Label roundNumber;

    void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        // label = rootVisualElement.Q<Label>(name: "Health");
        heartContainers = rootVisualElement.Q<GroupBox>("HeartGroupBox").Children().ToArray();
        formsEarnedBox = rootVisualElement.Q<VisualElement>("FormEarnedBox");
        roundNumber = rootVisualElement.Q<Label>("RoundNumber");
    }

    public void Init(Player player, GameController gameController)
    {
        player.OnCurrentHealthChange += CurrentHealthChange;
        player.OnCurrentMaxHealthChange += MaxHealthChange;
        player.OnCurrentLearnedTypesChange += CurrentFormsChange;
        gameController.OnRoundNumberChange += UpdateRoundNumber;
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

    void CurrentFormsChange(List<Type> types)
    {
        formsEarnedBox.Clear();
        foreach (Type type in types)
        {

            TemplateContainer formEarned = formEarnedTextBox.Instantiate();
            Label label = formEarned.Q<Label>("Form");
            label.text = TypeMethods.GetNameFromType(type);
            formsEarnedBox.Add(formEarned);
        }
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

    void UpdateRoundNumber(int newVal)
    {
        roundNumber.text = $"Round Number: {newVal}";
    }
}
