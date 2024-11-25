using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUp : MonoBehaviour
{
    [SerializeField] public float popUpTime = 2f, hideTime = 5;
    [SerializeField] TMP_Text fishName, fishWeightAndLength;
    [SerializeField] Transform gameManager;
    [SerializeField] LinePointAttacher linePointAttacher;
    [SerializeField] ItemManager itemManager;
    public static string FishType;//nanti diganti

    RodManager rodManager;
    void Start()
    {
        rodManager = gameManager.GetComponent<RodManager>();
    }
    public void SetText(string name, float weight, float length)
    {
        fishName.text = name;
        fishWeightAndLength.text = weight + "kg\n" + length + "m";
    }
    public void Show()
    {
        StartCoroutine(WaitCoroutine());
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(rodManager.equippedRod.RodMechanics.cast.bobberClone.GetComponent<Bobber>().duration);
        transform.DOScale(new Vector2(0.5f, 0.5f), popUpTime);
        yield return new WaitForSeconds(hideTime);
        transform.DOScale(new Vector3(0, 0), popUpTime);
        rodManager.equippedRod.SetState(RodRegistry.RodState.PreCast);
        linePointAttacher.Equip(itemManager.shop.UpgradeItems[ItemRegistry.UpgradeItemType.Rod].CurrentLevel);
    }
}
