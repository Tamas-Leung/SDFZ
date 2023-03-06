using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class WeaponWheelButtonController : MonoBehaviour
{

    public int Id;
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Selected()
    {
        selected = !selected;
        if (selected)
        {
            WeaponWheelController.weaponID = Id;
            WeaponWheelController.weaponWheelSelected = false;
        }
        else
        {
            WeaponWheelController.weaponID = 0;
        }
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = "";
    }

}
