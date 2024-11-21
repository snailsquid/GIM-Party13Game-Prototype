using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RodRegistry;

public class RodManager : MonoBehaviour
{
    public Rod equippedRod { get; private set; }
    public Bucket equippedBucket { get; private set; }
    public bool clickDebounce;
    [SerializeField] (int MinTime, int MaxTime) FishBite = (5, 10);
    [SerializeField] float MaxFishBiteTime = 20f;
    [SerializeField] private Transform referenceObject, waterObject;
    [SerializeField] Transform horizontalBar, verticalBar, fishableArea, target, bobberObject, hookBar, successBar, popUp, postRunPopup;
    [SerializeField] float bobberVelocity = 5f;
    TimeManager timeManager;
    CentralStateManager centralStateManager;
    void Awake()
    {
        equippedRod = new("Rod", RodRarity.Basic);
        centralStateManager = GetComponent<CentralStateManager>();
        timeManager = transform.GetComponent<TimeManager>();
        SetRod(RodRarity.Basic);
        equippedRod.EquipBait(BaitRegistry.Baits[BaitRegistry.BaitType.None]);
        EquipBucket(BucketRegistry.Rarity.Basic);
    }
    void Start()
    {

    }
    void EquipBucket(BucketRegistry.Rarity rarity)
    {
        equippedBucket = BucketRegistry.Buckets[rarity];
        equippedRod.SetBucket(equippedBucket);
    }
    void EquipBait(InventoryItem baitItem)
    {

    }
    void SetRod(RodRarity rodRarity)
    {
        equippedRod.SetRodRarity(rodRarity);
        Cast.Props castProps = new Cast.Props(horizontalBar, verticalBar, fishableArea, target, bobberObject, referenceObject, waterObject, bobberVelocity);
        Battle.Props battleProps = new Battle.Props(hookBar, successBar, MaxFishBiteTime, popUp);
        FishWait.Props fishWaitProps = new FishWait.Props(FishBite);
        PostFish.Props postFishProps = new PostFish.Props(centralStateManager, postRunPopup.GetComponent<PostRunPopup>(), equippedBucket);
        equippedRod.SetRodMechanic(new RodMechanics.Props(castProps, battleProps, fishWaitProps, postFishProps), timeManager, centralStateManager);
    }
    void Update()
    {
        if (centralStateManager.playerState == CentralStateManager.PlayerState.Rod)
        {

            if (Input.GetMouseButton(0))
            {
                if (!clickDebounce)
                {
                    clickDebounce = true;
                    OnClick();
                }

            }

            else
            {
                if (clickDebounce)
                {
                    clickDebounce = false;
                }
            }
            equippedRod.Update();
        }
    }
    void OnClick()
    {
        if (centralStateManager.playerState == CentralStateManager.PlayerState.Rod)
        {

            equippedRod.OnClick();
        }
    }
}
