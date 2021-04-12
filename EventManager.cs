using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public List<BoardPuzzle> puzzles = new List<BoardPuzzle>();
    public int level = 1;
    public int levelMax = 0;
    public Movement movement;
    public GameObject tips;

    //put all the puzzles in a ordered list and set all but the first one as inactive
    private void Awake()
    {
        puzzles = FindObjectsOfType<BoardPuzzle>().ToList();
        puzzles = puzzles.OrderBy(p => p.name).ToList();
        for (int i = 1; i < puzzles.Count; i++)
        {
            puzzles[i].gameObject.SetActive(false);
        }

        levelMax = puzzles.Count;
    }

    void Start()
    {
        InvokeRepeating("checkPuzzles", 0f, 1f);
    }

    //every second check if the first puzzle in the list is active and if it isn't spawn the next one
    void checkPuzzles()
    {
        if (!puzzles[0].gameObject.activeInHierarchy)
        {
            updateList(); }
    }

    //pop the first element in the list and set the next one active
    void updateList()
    {
        level++;
        movement.lastPosition = puzzles[0].transform.position;
        puzzles.RemoveAt(0);
        puzzles[0].gameObject.SetActive(true);
    }

    //exit game on esc key
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }
}
