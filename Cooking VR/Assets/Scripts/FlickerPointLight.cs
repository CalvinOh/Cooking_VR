using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerPointLight : MonoBehaviour
{

    private Light MyLight;
    private float BaseIntensity;
    [SerializeField]
    private float FlickerAmount = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        MyLight = GetComponent<Light>();
        BaseIntensity = MyLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        MyLight.intensity = BaseIntensity + Random.Range(-FlickerAmount,FlickerAmount);
    }
}
