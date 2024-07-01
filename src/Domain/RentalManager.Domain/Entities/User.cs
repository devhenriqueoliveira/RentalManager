using System.Net.Mail;
using System.Text;

namespace RentalManager.Domain.Entities
{
    public class User : Entity
    {
        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Role Role { get; private set; }

        #endregion

        #region Constructors

        public User(
            string firstName, 
            string lastName, 
            string login, 
            string password, 
            string email, 
            DateTime birthDate,
            Role role)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First Name cannot be empty or null.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last Name cannot be empty or null.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login cannot be empty or null.", nameof(login));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty or null.", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.", nameof(email));

            if (birthDate >= DateTime.Now)
                throw new ArgumentException("Date of birth must be in the past.", nameof(birthDate));


            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = HashPassword(password);
            Email = email;
            BirthDate = birthDate;
            Role = role;
        }

        #endregion

        #region Private Methods
        private static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
        #endregion

        #region Public Methods
        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("New email cannot be empty or null.", nameof(newEmail));

            if (!IsValidEmail(newEmail))
                throw new ArgumentException("Invalid email format.", nameof(newEmail));

            Email = newEmail;
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (HashPassword(currentPassword) != Password)
                throw new UnauthorizedAccessException("Current password is incorrect.");

            Password = HashPassword(newPassword);
        }

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        #endregion
    }
}
