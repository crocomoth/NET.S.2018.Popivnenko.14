using NET.S._2018.Popivnenko._14.BanlLibInterfaces.Entities;

namespace NET.S._2018.Popivnenko._14.BankService.Interface
{
    public interface IBankAccountService
    {
        void AddNewAccount(string name, string surname, CardType cardType);

        string GenerateNewNumber();

        void AddFundsToAccount(string number, decimal value);

        void WithdrawFundsFromAccount(string number, decimal value);

        void SetInavtive(string number);

        void SetActive(string number);
    }
}
