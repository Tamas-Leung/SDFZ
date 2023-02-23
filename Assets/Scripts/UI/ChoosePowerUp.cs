using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoosePowerUp : MonoBehaviour
{
    private Button option0;
    private Button option1;
    private Button option2;

    private Label label0;
    private Label label1;
    private Label label2;

    private GameController gameController;
    private Action<PowerUp> callbackFunction;
    private PowerUp[] options;

    private void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        option0 = rootVisualElement.Q<Button>(name: "Option0");
        option1 = rootVisualElement.Q<Button>(name: "Option1");
        option2 = rootVisualElement.Q<Button>(name: "Option2");

        label0 = rootVisualElement.Q<Label>(name: "Label0");
        label1 = rootVisualElement.Q<Label>(name: "Label1");
        label2 = rootVisualElement.Q<Label>(name: "Label2");

        gameController = FindObjectOfType<GameController>();

        option0.RegisterCallback<ClickEvent>(ev => OnButtonPress(0));
        option1.RegisterCallback<ClickEvent>(ev => OnButtonPress(1));
        option2.RegisterCallback<ClickEvent>(ev => OnButtonPress(2));
    }

    public void Init(PowerUp[] options, Action<PowerUp> callback)
    {
        callbackFunction = callback;
        this.options = options;
        label0.text = PowerUpMethods.GetPowerUpString(options[0]);
        label1.text = PowerUpMethods.GetPowerUpString(options[1]);
        label2.text = PowerUpMethods.GetPowerUpString(options[2]);
    }

    private void OnButtonPress(int index)
    {
        callbackFunction.Invoke(this.options[index]);
        Destroy(gameObject);
    }
}
