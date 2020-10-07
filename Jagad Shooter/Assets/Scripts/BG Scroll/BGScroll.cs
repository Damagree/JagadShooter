using UnityEngine;

public class BGScroll : MonoBehaviour
{

    public float scrollSpeed = .1f;
    private MeshRenderer meshRenderer;
    private float XScroll;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        XScroll = scrollSpeed * Time.time;

        Vector2 offset = new Vector2(XScroll, 0f);
        meshRenderer.sharedMaterial.mainTextureOffset = offset;
    }
}
