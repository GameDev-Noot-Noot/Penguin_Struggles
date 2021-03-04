using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float _melt_time;
    public float melt_time
    {
        get
        {
            return _melt_time;
        }
        set
        {
            _melt_time = melt_time
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy_itself()
    {
        Destroy(this);
    }
}
