using Multilayer_Backprop;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
///     This component handles AI's movement using neural-net trained in scene "TrainingScene Version 2" <br></br>
///     Uses <see cref="AINet"/> to compute its region
/// </summary>
public class PlayerNerualNet : MonoBehaviour
{
    public int PlayerVelocity = 3;
    public Transform[] Regions;
    public string FilePath;

    private NeuralNet AI;
    private PlayerControl controls;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        if (controls == null)
            Init();
    }

    private void Init()
    {
        controls = GetComponent<PlayerControl>();

        AI = new NeuralNet();
        AI.Init(3, new int[] { 8, 4 });
        AI.Load(FilePath);
        Debug.Log("Cargó red neuronal (backprop) de " + name);
    }

    public void Predict(Vector3 input)
    {
        string binary = AI.Compute(input);
        int index = 0, _class = System.Convert.ToInt32(binary, 2);

        switch (_class)
        {
            case 0: index = 0; break;
            case 1: index = 1; break;
            case 2: index = 2; break;
            case 4: index = 3; break;
            case 8: index = 4; break;
            default: Debug.LogError("Returned unknown class in prediction! =" + _class.ToString()); break;
        }
        Debug.Log("Predice Clase " + _class.ToString() + "  BINARIO=" + binary);

        //transform.position = Regions[index].position;
        // start moving the AI
        StartCoroutine(MoveToClass(Regions[index].position));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private IEnumerator MoveToClass(Vector3 position)
    {
        bool keepMoving = true;
        float dist;
        Vector3 dir;
        while (keepMoving)
        {
            dir = position - transform.position;
            dist = dir.magnitude;

            transform.position += dir.normalized * PlayerVelocity * Time.deltaTime;

            keepMoving = dist > 0.5f;
            yield return new WaitForEndOfFrame();
        }
    }
}
