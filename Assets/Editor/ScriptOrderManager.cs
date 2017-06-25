﻿using System;
using UnityEditor;

[InitializeOnLoad]
public class ScriptOrderManager
{
    static ScriptOrderManager ()
    {
        foreach (var monoScript in MonoImporter.GetAllRuntimeMonoScripts())
        {
            if (monoScript.GetClass() == null) continue;

            foreach (var attribute in Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(ScriptOrder), true))
            {
                var currentOrder = MonoImporter.GetExecutionOrder(monoScript);
                var newOrder = ((ScriptOrder)attribute).Order;
                if (currentOrder != newOrder)
                    MonoImporter.SetExecutionOrder(monoScript, newOrder);
            }
        }
    }
}
