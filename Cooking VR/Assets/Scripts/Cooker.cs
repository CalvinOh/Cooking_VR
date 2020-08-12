using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cooker : MonoBehaviour
{
    [SerializeField]
    protected bool _IsHeated;
    public bool IsHeated
    {
        get
        {
            return _IsHeated;
        }
        private set
        {
            _IsHeated = value;
            BroadcastCook();
        }
    }

    public byte testHeated;

    [SerializeField]
    protected List<CookableFood> itemsInTrigger;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        itemsInTrigger = new List<CookableFood>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CookableFood>(out CookableFood cf))
        {
            this.AddItemInTrigger(cf);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CookableFood>(out CookableFood cf))
        {
            this.RemoveItemInTrigger(cf);
        }
    }

    protected virtual void AddItemInTrigger(CookableFood item)
    {
        if (!itemsInTrigger.Contains(item))
        {
            itemsInTrigger.Add(item);
            Debug.Log($"{item.gameObject.name} was added to the {this.gameObject.name}'s list of items in trigger");
            if (this._IsHeated)
            {
                item.StartCook();
                Debug.Log($"{item.gameObject.name} was told to start cooking");
            }
        }
    }

    protected virtual void RemoveItemInTrigger(CookableFood item)
    {
        if (itemsInTrigger.Contains(item))
        {
            itemsInTrigger.Remove(item);
            item.stopCook();
        }
    }

    public virtual void BroadcastCook()
    {
        foreach (var v in itemsInTrigger)
        {
            if (IsHeated)
                v.StartCook();
            else
                v.stopCook();
        }
    }

    public virtual void StartCooking()
    {
        IsHeated = true;
    }

    public virtual void StopCooking()
    {
        IsHeated = false;
    }
}
