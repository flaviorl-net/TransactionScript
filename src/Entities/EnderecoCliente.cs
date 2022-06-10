public class EnderecoCliente : Entity<Guid>
{
    public string Logradouro { get; private set; }
    public string Endereco { get; private set; }
    public int Numero { get; private set; }
    public string Complemento { get; private set; }
    public Guid ClienteId { get; private set; }

    public EnderecoCliente() {}
        
    public EnderecoCliente(Guid id, string logradouro, string endereco, int numero, string complemento, Guid clienteId)
    {
        Id = id;
        Logradouro = logradouro;
        Endereco = endereco;
        Numero = numero;
        Complemento = complemento;
        ClienteId = clienteId;
        ValidationResult = new ValidationResult();

        Validar();
    }

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Logradouro))
            ValidationResult.Errors.Add("Informe o Logradouro.");
        
        if (string.IsNullOrWhiteSpace(Endereco))
            ValidationResult.Errors.Add("Informe o Endereco do cliente.");

        if (Numero == 0)
            ValidationResult.Errors.Add("Informe o Numero.");

        ValidationResult.IsValid = true;

        if (ValidationResult.Errors.Count > 0)
        {
            ValidationResult.IsValid = false;
        }
    }

}