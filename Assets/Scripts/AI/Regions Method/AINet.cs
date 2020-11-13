using Multilayer_Backprop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AiNetwork", order = 1)]

public class AINet : ScriptableObject
{
    public string Name;
    public NeuralNet Network;
}
