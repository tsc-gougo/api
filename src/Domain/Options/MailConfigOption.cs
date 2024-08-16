﻿namespace Domain.Options;

public class MailConfigOption
{
    public string? From { get; set; }

    public string? ContactMail { get; set; }

    public string? Host { get; set; }

    public int Port { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool UseStartTls { get; set; }

    public bool UseSsl { get; set; }
}