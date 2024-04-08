namespace ProiectMPDA
{
    public class Pair<T1, T2>(T1 firstItem, T2 secondItem)
    {
        public T1 FirstItem { get; set; } = firstItem;
        public T2 SecondItem { get; set; } = secondItem;
    }
}
