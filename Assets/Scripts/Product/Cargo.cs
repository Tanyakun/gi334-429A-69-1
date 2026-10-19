using UnityEngine;

[System.Serializable]
public class Cargo
{
	[SerializeField]
	private int productId;
	public int ProductID { get { return productId; } set { productId = value; } }

	[SerializeField]
	private int quantity;
	public int Quantity { get { return quantity; } set { quantity = value; } }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public Cargo(int id, int q)
	{
		productId = id;
		quantity = q;
	}
}
