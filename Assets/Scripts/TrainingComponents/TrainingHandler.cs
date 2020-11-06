using UnityEngine;

public class TrainingHandler : MonoBehaviour
{
    public int TotalIndividuals = 2;
    public GameObject Indiviual;

    // Start is called before the first frame update
    void Start()
    {
        DuplicatePlayers();   
    }

    /// <summary>
    ///     Creates players population
    /// </summary>
    public void DuplicatePlayers()
    {
        for (int i = 0; i < TotalIndividuals - 1; i++)
        {
            GameObject newInidividual = Instantiate(Indiviual);
            Vector3 pos = newInidividual.transform.position;
            pos.z = -(i + 1);
            newInidividual.transform.position = pos;
        }
    }
}
