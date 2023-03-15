using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{

    public Animator anim;
    public static bool weaponWheelSelected = false;
    public static int weaponID;
    private GameController gameController;
    private Player player = null;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = gameController.getPlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
