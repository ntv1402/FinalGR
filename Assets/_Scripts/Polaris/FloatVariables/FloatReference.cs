using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloatReference
{
    public bool useConstant;
    public float ConstantValue;
    public int intConstantValue;
    public FloatVariable Variable;
    public FloatVariable intVariable;

    public float Value
    {
        get { return useConstant ? ConstantValue : Variable.value; }
    }

    public int intValue
    {
        get { return useConstant ? intConstantValue : Variable.intValue; }
    }
}
