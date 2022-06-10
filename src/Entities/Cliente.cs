public class Cliente : Entity<Guid>, IAggregateRoot
{
    public string Nome { get; private set; }
    public string SobreNome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public EnderecoCliente EnderecoCliente { get; private set; }

    protected Cliente() {}

    public Cliente(Guid id, string nome, string sobreNome, DateTime dataNascimento)
    {
        Id = id;
        Nome = nome;
        SobreNome = sobreNome;
        DataNascimento = dataNascimento;
        ValidationResult = new ValidationResult();

        Validar();
    }

    public void AdicionarEndereco(Guid id, string logradouro, string endereco, int numero, string complemento, Guid clienteId)
    {
        ValidationResult.IsValid = true;

        var enderecoCliente = new EnderecoCliente(id, logradouro, endereco, numero, complemento, clienteId);

        if (!enderecoCliente.ValidationResult.IsValid)
        {
            ValidationResult.IsValid = false;
            ValidationResult.Errors.AddRange(enderecoCliente.ValidationResult.Errors);
            return;
        }
        
        EnderecoCliente = enderecoCliente;
    }

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            ValidationResult.Errors.Add("Informe o Nome do cliente.");
        
        if (string.IsNullOrWhiteSpace(SobreNome))
            ValidationResult.Errors.Add("Informe o Sobrenome do cliente.");

        if (DataNascimento.Year < 1900)
            ValidationResult.Errors.Add("Informe a data de nascimento do cliente.");

        ValidationResult.IsValid = true;

        if (ValidationResult.Errors.Count > 0)
        {
            ValidationResult.IsValid = false;
        }
    }
}