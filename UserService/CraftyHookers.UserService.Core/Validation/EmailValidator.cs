using System.Net.Mail;

namespace CraftyHookers.UserService.Core.Validation
{
    public static class EmailValidator
    {
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var address = new MailAddress(email);

                // MailAddress accepts display-name-decorated input (e.g. "Name <a@b.com>") and
                // hostnames with no dot (e.g. user@localhost); reject both for a plain email field.
                return address.Address == email && address.Host.Contains('.');
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
