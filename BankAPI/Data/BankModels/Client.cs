using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // video 11
using System.Text.Json.Serialization; // video 11

namespace BankAPI.Data.BankModels;

public partial class Client
{

    public Client()
    {
        Accounts = new HashSet<Account>();
    }

    public int Id { get; set; }

    [MaxLength(200, ErrorMessage = "El nombre debe ser menor a 200 caracteres.")] // video 11
    public string Name { get; set; } = null!; // No nulo

    [MaxLength(40, ErrorMessage = "El número de teléfono debe ser menor a 40 caracteres.")]
    public string PhoneNumber { get; set; } = null!; // No nulo


    [MaxLength(50, ErrorMessage = "El email debe ser menor a 50 caracteres.")]
    [EmailAddress(ErrorMessage = "El formato de correo es incorrecto.")]
    public string? Email { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
