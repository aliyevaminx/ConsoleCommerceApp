using Core.Security;

namespace Application.Services.Concrete;

public class LoginService : ILoginService
{
    private readonly UnitOfWork _unitOfWork;

    public LoginService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        bool isPasswordMatch;

        if (existedCustomer is not null)
        {
            if (PasswordVerifier.VerifyPassword(password, existedCustomer.Password, existedCustomer.Salt))
            {
                Messages.SuccessLoginMessage();
                return existedCustomer;
            }
            return null;
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
        bool isPasswordMatch;

        if (existedSeller is not null)
        {
            if (PasswordVerifier.VerifyPassword(password, existedSeller.Password, existedSeller.Salt))
            {
                Messages.SuccessLoginMessage();
                return existedSeller;
            }
            return null;
        }
        return null;
    }


}
