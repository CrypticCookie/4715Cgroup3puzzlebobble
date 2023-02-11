using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ceilingScript : MonoBehaviour
{
    private static int turnCount = 0;
    public int ceilingMoves = 1;
    new Renderer renderer;
    private Vector2 size;
    public CannonScript Cannon;

    void Start()
    {
        turnCount = 0;
        ceilingMoves = 1;
        renderer = gameObject.GetComponent<Renderer>();
        size = renderer.bounds.size;
    }

    public void checkCeiling()
    {
        turnCount = turnCount + 1;
        Collider2D[] ceilingChecker = Physics2D.OverlapBoxAll(transform.position, size * 1.5f, 0);
        foreach (var ceilingBubble in ceilingChecker)
        {
            if(ceilingBubble.tag != "Wall" && ceilingBubble.tag != "Roof")
            {
                activeChecker ceilingChain = ceilingBubble.GetComponent<Collider2D>().GetComponent<activeChecker>();
                ceilingChain.checkCeilingChain(turnCount);
            }
        }
        var allBubbles = FindObjectsOfType<activeChecker>();
        foreach(var activeBubbles in allBubbles)
        {
            activeChecker ceilingChain = activeBubbles.GetComponent<Collider2D>().GetComponent<activeChecker>();
            ceilingChain.ceilingPopper(turnCount);
        }
        if(turnCount == (ceilingMoves * 8))
        {
            ceilingMoves = ceilingMoves + 1;
            foreach(var activeBubbles in allBubbles)
            {
                activeBubbles.transform.position = activeBubbles.transform.position + new Vector3(0, -.875f, 0);
            }
            Debug.Log("Ceiling Moved!");
            transform.position = transform.position + new Vector3(0, -.875f, 0);
        }
        Cannon.StartCoroutine(Cannon.waitForList());
    }
}
