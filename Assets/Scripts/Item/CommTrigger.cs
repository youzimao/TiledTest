using UnityEngine;
using UnityEngine.Events;

public class CommTrigger : MonoBehaviour
{
    public Vector2 size;
    public event UnityAction<Collider2D> colliderTriggerAction = delegate { };
    public event UnityAction triggerAction = delegate { };
    public Color color;
    Vector3 o;
    private string cTag= "Player";
    private void Start()
    {
        o = transform.position;
        SetSize(this.size);

    }
    public void SetSize(Vector2 size)
    {
        this.size = size;
        BoxCollider2D c = GetComponent<BoxCollider2D>();
        c.size = size;
    }
    public void SetCheckTag(string tag= "Player")
    {
        this.cTag = tag;
    }
    private void Update()
    {
        MyUtilities.DebugDraw(new Vector2(o.x, o.y), size, color);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(cTag))
        {
            colliderTriggerAction.Invoke(collision);
            triggerAction.Invoke();
        }
    }

}
