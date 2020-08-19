using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class CookableFood : MonoBehaviour
{
    protected bool currentlyCooking;
    public float cookedTime = 0;
    private ManualStack manualStack;
    public OrderManager.Ingridents ingridentName;
    public byte currentStage = 0;
    public byte stages;
    public ushort[] stageRefs;
    [SerializeField]
    protected List<GameObject> VisualObjects;// = new List<GameObject>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        manualStack = this.GetComponent<ManualStack>();
        this.ingridentName = manualStack.ingredientName;
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
        if(currentlyCooking)
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.TryGetComponent<PanScript>(out PanScript ps))
        //{
        //    other.gameObject.GetComponent<PanScript>().AddItemInTrigger(this);
        //}
    }

    virtual public void StartCook()
    {
        currentlyCooking = true;
        // play sound?
    }

    public void stopCook()
    {
        currentlyCooking = false;
        // stop play sound?
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
