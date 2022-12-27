using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform _segmentPrefab;
    public int initialSize = 3;

    private void Start()
    {
        ResetGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
    }
    private void FixedUpdate()
    {
        for(int i=_segments.Count-1; i>0;i--)
        {
            _segments[i].position = _segments[i-1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x)+direction.x,
            Mathf.Round(transform.position.y)+direction.y,
            0.0f );
    }
    private void Grow()
    {
        Transform segment = Instantiate( _segmentPrefab );
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add( segment );
    }
    private void ResetGame()
    {
        for(int i=1; i<_segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);
        for(int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(_segmentPrefab));
        }
        transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }else if(collision.tag == "Obstacle")
        {
            ResetGame();
        }
    }
}
