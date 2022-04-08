namespace Coyote.Catalog.Products.Domain.Model;

public class ProductFeature
{
    public ProductFeature(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; private set; }
    public object Value { get; private set; }
}

public class ProductFeature<TValue> : ProductFeature
{
    public ProductFeature(string name, TValue value) : base(name, value)
    {
        Value = value;
    }

    public new TValue Value { get; private set; }
}
