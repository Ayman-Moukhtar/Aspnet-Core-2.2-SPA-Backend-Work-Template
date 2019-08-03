using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWE.JOIN.CrossCutting.SmtpEmailSender
{
    public class EmailProperties
    {
        public EmailProperties()
        {
            Bcc = new List<string>();
            Attachments = new List<Tuple<string, byte[]>>();
        }
        [Required]
        [EmailAddress]
        public string To { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public string Password { get; set; }

        public List<string> Bcc { get; set; }

        public List<Tuple<string, byte[]>> Attachments { get; set; }
    }
}
