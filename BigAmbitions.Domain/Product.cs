namespace BigAmbitions.Domain;
public class Product
{
    public string Name { get; set; }

    //public int SoldLast7Days { get; set; }

    //public int QtiToBuy { get; private set; }

    public int BusinessBoxesNeeded { get; set; }

    //public void SetQtiToBuy(int businessDaysOpened, int qtiPerBox)
    //{
    //    QtiToBuy = (int)Math.Ceiling((1.0 * SoldLast7Days / businessDaysOpened) / qtiPerBox) * qtiPerBox;
    //}

    //public void SetBusinessBoxesNeeded(int qtiPerBox)
    //{
    //    BusinessBoxesNeeded = (int)Math.Ceiling(1.0 * QtiToBuy / qtiPerBox);
    //}
}
