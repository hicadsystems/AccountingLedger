
using NavyAccountWeb.Models;

namespace NavyAccountWeb.IServices
{
	public interface IEmailSenderService
	{
		string SendEmailAsync(EmailModel mailRequest);
		string SendMultipleEmail(EmailModel2 mailRequest);

    }
}
