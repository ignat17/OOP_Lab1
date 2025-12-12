public class Product
{
    private string _name;
    private Category _category;
    private decimal _price;
    private int _stock;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public Category Category
    {
        get => _category;
        set => _category = value;
    }

    public decimal Price
    {
        get => _price;
        set => _price = value;
    }

    public int Stock
    {
        get => _stock;
        set => _stock = value;
    }

    public void restock(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Кiлькiсть має бути бiльшою за 0.");

        _stock += amount;
    }

    public void purchase(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Кiлькiсть має бути бiльшою за 0.");

        if (amount > _stock)
            throw new InvalidOperationException("Недостатньо товару на складi.");

        _stock -= amount;
    }

    public void discount(double percent)
    {
        if (percent < 0 || percent > 90)
            throw new ArgumentException("Знижка має бути в межах 0–90%.");

        _price -= _price * (decimal)(percent / 100);
    }

    public override string ToString()
    {
        return $"{Name,-20} | {Category,-12} | {Price,8:F2} | {Stock,5}";
    }
}
