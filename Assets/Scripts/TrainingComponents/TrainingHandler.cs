using System.Collections;
using UnityEngine;

public class TrainingHandler : MonoBehaviour
{
    public int TotalPlayers = 10;
    public int DisabledPlayers = 0;
    public GameObject Individual;
    public PlayerAI[] Individuals;
    public GameObject Parent;
    private DiferentialEvolution DiferentialEvolution;
    
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        Individuals = new PlayerAI[TotalPlayers];
        pos = Individual.transform.position;
        DuplicatePlayers();
        StartCoroutine(CheckDisabledPlayers());
    }

    /// <summary>
    ///     Creates players population
    /// </summary>
    public void DuplicatePlayers()
    {
        for (int i = 0; i < TotalPlayers - 1; i++)
        {
            pos.z = -(i + 1);
            GameObject newInidividual = Instantiate(Individual, pos, Quaternion.identity, Parent.transform) as GameObject; ;
            Individuals[i] = newInidividual.GetComponent<PlayerAI>();
            newInidividual.GetComponentInChildren<PlayerAI>().InitNeuralNet();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckDisabledPlayers()
    {
        while(DisabledPlayers < TotalPlayers)
        {
            yield return new WaitForEndOfFrame();
        }

        // start diferential evolution
        
    }
}
