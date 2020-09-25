using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class CookableFood : MonoBehaviour
{
    protected bool currentlyCooking;
    protected bool burnt = false;
    public float cookedTime = 0;
    protected ManualStack manualStack;
    public OrderManager.Ingridents ingridentName
    {
        get
        {
            return manualStack.ingredientName;
        }
        protected set
        {
            manualStack.ingredientName = value;
        }
    }

    public byte currentStage = 0;
    public byte stages;
    public ushort[] stageRefs;
    [SerializeField]
    protected List<GameObject> VisualObjects;// = new List<GameObject>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        manualStack = this.GetComponent<ManualStack>();
        if (VisualObjects.Count == 0)
            VisualObjects = new List<GameObject>();
    }

    protected virtual void OnEnable()
    {
        if (stages <= 0)
            stages = 1;

        stageRefs = new ushort[stages];

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(currentlyCooking && !burnt)
        {
            cookedTime += Time.deltaTime;
            CheckIfSwitchVisual();
        }
    }

    private void CheckIfSwitchVisual()
    {
        if (currentStage < (stageRefs.Length - 2)) // Check if reached final stage
        {
            if (cookedTime > stageRefs[(currentStage + 1)])
            {
                currentStage++;
                SwitchVisualObject();
            }
        }
    }

    virtual public void StartCook()
    {
        currentlyCooking = true;
        // sound triggers in CookablePatty/OnionRing scripts
    }

    public void stopCook()
    {
        currentlyCooking = false;

        AkSoundEngine.PostEvent("Burger_Cook_Stop", gameObject);
        AkSoundEngine.PostEvent("Oil_Fry_Stop", gameObject);
    }

    protected virtual void AssignStageRefs()
    {
        this.stages = (byte)VisualObjects.Count;
    }

    virtual protected void SwitchVisualObject()
    {
        foreach (GameObject VO in VisualObjects)
        {
            VO.SetActive(false);
        }
        VisualObjects[currentStage].SetActive(true);
    }
}
