using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private bool isInit;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        disappearTimer = 0.3f;
    }

    public void Setup(float damageAmount, bool advantageDamage, Type damageType)
    {
        Debug.Log(damageType);
        textMesh.SetText(Mathf.RoundToInt(damageAmount).ToString());
        if (advantageDamage) textMesh.fontSize = 5f;
        Debug.Log(TypeMethods.GetColorFromType(damageType));
        textColor = TypeMethods.GetColorFromType(damageType);
        textMesh.color = textColor;
        isInit = true;
    }


    public void Update()
    {
        if (!isInit) return;
        transform.position += new Vector3(0, 2f) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= 3f * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
