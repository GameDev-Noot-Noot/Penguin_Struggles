using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class melt_bar : MonoBehaviour
{
    private Image melt;
    private float size;
    private float melt_time;
    private float increment;

    void Start()
    {
        melt = transform.GetChild(1).GetComponent<Image>();
        size = get_bar_size();
    }

    void Update()
    {
        size -= Time.deltaTime * 1 / melt_time;
        melt.fillAmount = size;

        if (melt.fillAmount == 0f)
        {
            Destroy(gameObject);
        } 
    }

    public float get_bar_size()
    {
        return melt.fillAmount;
    }

    public void set_melt_time(float value)
    {
        melt_time = value;
    }
}
