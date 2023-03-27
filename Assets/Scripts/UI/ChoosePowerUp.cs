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
    private AudioSource audioSource;
    public AudioClip selectAudioClip;

    private void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        audioSource = Camera.main.GetComponent<AudioSource>();
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
        label0.style.borderBottomColor = PowerUpMethods.GetColorFromRarity(options[0]);
        label0.style.borderTopColor = PowerUpMethods.GetColorFromRarity(options[0]);
        label0.style.borderRightColor = PowerUpMethods.GetColorFromRarity(options[0]);
        label0.style.borderLeftColor = PowerUpMethods.GetColorFromRarity(options[0]);
        label1.text = PowerUpMethods.GetPowerUpString(options[1]);
        label1.style.borderBottomColor = PowerUpMethods.GetColorFromRarity(options[1]);
        label1.style.borderTopColor = PowerUpMethods.GetColorFromRarity(options[1]);
        label1.style.borderRightColor = PowerUpMethods.GetColorFromRarity(options[1]);
        label1.style.borderLeftColor = PowerUpMethods.GetColorFromRarity(options[1]);
        label2.text = PowerUpMethods.GetPowerUpString(options[2]);
        label2.style.borderBottomColor = PowerUpMethods.GetColorFromRarity(options[2]);
        label2.style.borderTopColor = PowerUpMethods.GetColorFromRarity(options[2]);
        label2.style.borderRightColor = PowerUpMethods.GetColorFromRarity(options[2]);
        label2.style.borderLeftColor = PowerUpMethods.GetColorFromRarity(options[2]);
    }

    private void OnButtonPress(int index)
    {
        audioSource.PlayOneShot(selectAudioClip, 0.5f);
        callbackFunction.Invoke(this.options[index]);
        Destroy(gameObject);
    }
}
