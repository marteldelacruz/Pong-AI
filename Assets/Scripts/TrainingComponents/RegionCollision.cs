using MathNet.Numerics.LinearAlgebra;
using Multilayer_Backprop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionCollision : MonoBehaviour
{
    public bool IsRecording = false;

    public BoxCollider RegionDummy1;
    public BoxCollider RegionDummy2;
    public BoxCollider RegionDummy3;
    public BoxCollider RegionDummy4;
    public BoxCollider RegionDummy5;


    private Bounds[] regions;
    private int[] classes;

    [HideInInspector]
    public Vector2 BallPosition, BallDir;

    private List<Vector4> dataset;

    private Matrix<float> datasetInputs, datasetDesireds;
    public NeuralNet NeuralNetwork;

    private const int OUTPUT_NEURONS = 4;
    private const string FILENAME = "Dataset.csv";

    private void Start()
    {
        Vector3 size = RegionDummy1.bounds.size;
        regions = new Bounds[5] { 
            new Bounds(RegionDummy1.bounds.center, size), 
            new Bounds(RegionDummy2.bounds.center, size), 
            new Bounds(RegionDummy3.bounds.center, size), 
            new Bounds(RegionDummy4.bounds.center, size), 
            new Bounds(RegionDummy5.bounds.center, size)
        };
        classes = new int[5] { 0, 1, 2, 4, 8 };

        dataset = new List<Vector4>();

        NeuralNetwork = new NeuralNet();
        NeuralNetwork.Init(3, new int[] { 8, OUTPUT_NEURONS });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            var bm = collision.gameObject.GetComponent<BallMovement>();
            bm.BallDirection.x = 1;
            bm.IncreaseBallSpeed();

            if (IsRecording)
                SaveWithClass(bm.transform.position);
        }
    }

    private void SaveWithClass(Vector3 ballPos)
    {
        Vector4 newSample;
        for (int i = 0; i < regions.Length; i++)
        {
            if (regions[i].Contains(ballPos))
            {
                newSample = new Vector4(BallDir.x, BallDir.y, BallPosition.y, classes[i]);
                if (!dataset.Contains(newSample))
                {
                    dataset.Add(newSample);
                    WriteDataset(newSample);
                    Debug.Log(string.Format("Agregó con deseada {0} TOTAL-PUNTOS={1}", classes[i], dataset.Count));
                }
                break;
            }
        }
    }

    public void BeginTraining()
    {
        BuildDataset();
        StartCoroutine(
            NeuralNetwork.BackpropagationTraining(datasetInputs, datasetDesireds)
        );
    }

    private void BuildDataset()
    {
        var M = Matrix<float>.Build;
        int i = 0;

        datasetInputs = M.Dense(dataset.Count, 3);
        datasetDesireds = M.Dense(dataset.Count, OUTPUT_NEURONS);

        foreach(Vector<float> row in datasetInputs.EnumerateRows())
        {
            // set each column with inputs...
            row[0] = dataset[i].x;
            row[1] = dataset[i].y;
            row[2] = dataset[i].z;
            i++;
        }
        i = 0;
        foreach(Vector<float> row in datasetDesireds.EnumerateRows())
        {
            // set each column with inputs...
            string binaryStr = System.Convert.ToString((int)dataset[i].w, 2).PadLeft(4, '0');
            row[0] = int.Parse(binaryStr[0].ToString());
            row[1] = int.Parse(binaryStr[1].ToString());
            row[2] = int.Parse(binaryStr[2].ToString());
            row[3] = int.Parse(binaryStr[3].ToString());
        }
    }

    private void WriteDataset(Vector4 sample)
    {
        using (var writer = new System.IO.StreamWriter(FILENAME, true))
        {
            string text = string.Format("{0:0.000},{1:0.000},{2:0.000},{3}", sample.x, sample.y, sample.z, (int)sample.w);
            writer.WriteLine(text);
            writer.Close();
        }
    }

    public void ReadDatasetFromFile()
    {
        try
        {
            using (var stream = new System.IO.StreamReader(FILENAME))
            {
                string contents = stream.ReadToEnd();
                string[] lines;

                stream.Close();

                // Separate each training sample
                lines = contents.Split('\n');

                // Prepare training data list
                BuildTrainingData(lines);
                Debug.Log("[TRAINING-DATA] Loaded!");
            }
        }
        catch (System.IO.IOException e)
        {
            Debug.LogError("[DEBUG]    File couldn't be read!");
            Debug.LogError(e.Message);
        }
    }

    /// <summary>
    ///     Builds matrix with training data
    /// </summary>
    /// <param name="strData">  Each line in training's data file   </param>
    private void BuildTrainingData(string[] strData)
    {
        string[] numbers;
        string line;
        Vector4 newSample;

        for (int i = 0; i < strData.Length; i++)
        {
            line = strData[i].TrimEnd('\r');
            numbers = line.Split(',');

            if (line.Length > 0 && numbers.Length > 0)
            {
                newSample = new Vector4(
                    float.Parse(numbers[0]), float.Parse(numbers[1]),
                    float.Parse(numbers[2]), float.Parse(numbers[3])
                );
                dataset.Add(newSample);
            }
        }
    }
}
