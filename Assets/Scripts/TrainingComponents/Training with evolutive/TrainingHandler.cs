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
        DiferentialEvolution = new DiferentialEvolution();
        Individuals = new PlayerAI[TotalPlayers];
        pos = Individual.transform.position;
        DuplicatePlayers();

        // Start waiting...
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
            Individuals[i] = newInidividual.GetComponentInChildren<PlayerAI>();
            Individuals[i].InitNeuralNet();
        }
        // set last remaining AI
        Individuals[TotalPlayers - 1] = Individual.GetComponentInChildren<PlayerAI>();
    }

    /// <summary>
    ///     Waits until all players have been disbled, then starts diferential evolution algorithm
    /// </summary>
    public IEnumerator CheckDisabledPlayers()
    {
        while(DisabledPlayers < TotalPlayers)
            yield return new WaitForEndOfFrame();

        // start diferential evolution
        DiferentialEvolution.Individuals = Individuals;
        DiferentialEvolution.Algorithm();

        var xBest = DiferentialEvolution.GetBestPlayer();

        Debug.Log(string.Format("<size=22>  Best player at index {0} with Error={1}</size>", 
            xBest, Individuals[xBest].ErrorAvrg));

        // Restart all individuals
        foreach (PlayerAI p in Individuals)
            p.transform.parent.gameObject.SetActive(true);

        DisabledPlayers = 0;
        StartCoroutine(CheckDisabledPlayers());
    }
}
