public interface IEmailService
{
    bool EnviarEmail(string remetente, string[] destinatario, string assunto, string corpo);
}