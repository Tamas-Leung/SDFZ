using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{

    public Animator anim;
    public static bool weaponWheelSelected = false;
    public static int weaponID;
    private GameController gameController;
    private Player player = null;
    public AudioSource change;

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
            Debug.Log(player);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if (weaponWheelSelected)
        {
            change.Play();
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)
        {
            case 1:
                Debug.Log(player.currrentLearnedTypes);
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
