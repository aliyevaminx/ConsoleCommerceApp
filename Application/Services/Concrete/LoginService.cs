using Core.Security;

namespace Application.Services.Concrete;

public class LoginService : ILoginService
{
    private readonly UnitOfWork _unitOfWork;

    public LoginService()
    {
        _unitOfWork = new UnitOfWork();
    }

    public Admin IsAdminLoggedIn()
    {
    EnterEmailLine: Messages.InputMessage("email");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email) || !email.IsValidEmail())
        {
            Messages.InvalidInputMessage("Email");
            goto EnterEmailLine;
        }

    EnterPasswordLine: Messages.InputMessage("password");
        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMessage("Password");
            goto EnterPasswordLine;
        }

        var existedAdmin = _unitOfWork.Admins.GetAdminByEmail(email);

        if (existedAdmin is not null && existedAdmin.Password == password)
        {
            Messages.SuccessLoginMessage();
            return existedAdmin;
        }
        return null;

    }
    public Customer IsCustomerLoggedIn()
    {
    EnterEmailLine: Messages.InputMessage("email");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email) && !email.IsValidEmail())
        {
            Messages.InvalidInputMessage("Email");
            goto EnterEmailLine;
        }

    EnterPasswordLine: Messages.InputMessage("password");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMessage("Password");
            goto EnterPasswordLine;
        }

        var existedCustomer = _unitOfWork.Customers.GetCustomerByEmail(email);
        bool isPasswordMatch = PasswordVerifier.VerifyPassword(password, existedCustomer.Password, existedCustomer.Salt);

        if (existedCustomer is not null && isPasswordMatch)
        {
            Messages.SuccessLoginMessage();
            return existedCustomer;
        }
        return null;

    }
    public Seller IsSellerLoggedIn()
    {
    EnterEmailLine: Messages.InputMessage("email");
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email) && !email.IsValidEmail())
        {
            Messages.InvalidInputMessage("Email");
            goto EnterEmailLine;
        }

    EnterPasswordLine: Messages.InputMessage("password");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMessage("Password");
            goto EnterPasswordLine;
        }

        var existedSeller = _unitOfWork.Sellers.GetSellerByEmail(email);
        bool isPasswordMatch = PasswordVerifier.VerifyPassword(password, existedSeller.Password, existedSeller.Salt);

        if (existedSeller is not null && isPasswordMatch)
        {
            Messages.SuccessLoginMessage();
            return existedSeller;
        }
        return null;
    }
}
