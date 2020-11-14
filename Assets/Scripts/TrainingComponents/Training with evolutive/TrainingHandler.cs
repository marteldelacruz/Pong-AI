using System.Collections;
using UnityEngine;

/// <summary>
///     Manages training by using differential evolution 
/// </summary>
public class TrainingHandler : MonoBehaviour
{
    [Tooltip("Population of individuals")]
    public int TotalPlayers = 10;
    [Tooltip("Number of dead players")]
    public int DisabledPlayers = 0;
    [Tooltip("Base individual (will make copy of this gameObject)")]
    public GameObject Individual;
    [Tooltip("Copies of Individual will be set as children of this Parent")]
    public GameObject Parent;
    [Tooltip("Path where the best network will be saved")]
    public string FilePath;

    [HideInInspector]
    public PlayerAI[] Individuals;

    //  Algorithm handler
    private DiferentialEvolution DiferentialEvolution;
    private int MAX_GENERATIONS = 3, currentGeneration = 0;

    // Start is called before the first frame update
    void Awake()
    {
        DiferentialEvolution = new DiferentialEvolution();
        Individuals = new PlayerAI[TotalPlayers];
        DuplicatePlayers();

        // Start waiting...
        StartCoroutine(CheckDisabledPlayers());
    }

    /// <summary>
    ///     Creates players population
    /// </summary>
    public void DuplicatePlayers()
    {
        Vector3 pos = Individual.transform.position;
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
        Debug.Log("Corrutina inicia");
        //while(DisabledPlayers < TotalPlayers)
        yield return new WaitForSecondsRealtime(10);

        foreach (PlayerAI p in Individuals)
            p.transform.parent.gameObject.SetActive(false);

        // start diferential evolution
        DiferentialEvolution.Individuals = Individuals;
        DiferentialEvolution.Algorithm();

        var xBest = DiferentialEvolution.GetBestPlayer();

        Debug.Log(string.Format("<size=22>  Best player at index {0} with Error={1}</size>", 
            xBest, Individuals[xBest].ErrorAvrg));

        // Restart all individuals
        foreach (PlayerAI p in Individuals)
            p.transform.parent.gameObject.SetActive(true);

        if (currentGeneration < MAX_GENERATIONS)
        {
            currentGeneration++;
            DisabledPlayers = 0;
            StartCoroutine(CheckDisabledPlayers());
        }
        else
        {
            Debug.Log("TRAINING DONE");
            Individuals[xBest].Net.SaveToCsv(FilePath);
            Debug.Log("Saved to => " + FilePath);
        }
    }
}
