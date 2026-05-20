namespace myapi.Models;

public class Funcionario
{
    private string? nome;
    private int idade;
    private string? cargo;
    private string? departamento;
    
    public string? Nome
    {
        get { return nome; }
        set { nome = value; }
    }
    public int Idade
    {
        get { return idade; }
        set { idade = value; }
    }
    public string? Cargo
    {
        get { return cargo; }
        set { cargo = value; }
    }
    public string? Departamento
    {
        get { return departamento; }
        set { departamento = value; }
    }
}