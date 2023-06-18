namespace Bancocdis;

public class client : User, IPerson
{
    public char TaxRegime { get; set; }

    public client(){}

    public client(int ID, string Name, string Email, decimal Balance, char TaxRegime): base(ID, Name, Email, Balance)
    {
        this.TaxRegime=TaxRegime;
        SetBalance(Balance);
    }

    public override void SetBalance(decimal amount)
    {
        base.SetBalance(amount);

        if (TaxRegime.Equals('M'))
            Balance +=(amount*0.02m);
    }

    public override string ShowData()
    {
        return base.ShowData()+$", Regimen Fiscal: {this.TaxRegime}";
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

    public string GetCountry()
    {
        throw new NotImplementedException();
    }
}