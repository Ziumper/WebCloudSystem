using System.Collections.Generic;
using WebCloudSystem.Bll.Services.Emails.Models;

namespace WebCloudSystem.Bll.Services.Emails {
    public interface IEmailService {
        void Send(EmailMessage emailMessage);
	    List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}