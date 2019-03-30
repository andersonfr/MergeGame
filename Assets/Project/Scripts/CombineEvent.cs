
internal class CombineEvent
{
    public Item a, b;
    public CombineEvent(Item a, Item b)
    {
        this.a = a;
        this.b = b;
    }

    public bool CheckCombination()
    {
        return (a.level == b.level);
    }
}