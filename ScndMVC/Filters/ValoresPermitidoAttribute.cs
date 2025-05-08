using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class ValoresPermitidosAttribute : ValidationAttribute
{
    private readonly int[] _valoresPermitidos;

    public ValoresPermitidosAttribute(params int[] valoresPermitidos)
    {
        _valoresPermitidos = valoresPermitidos;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !_valoresPermitidos.Contains((int)value))
        {
            string valores = string.Join(", ", _valoresPermitidos);
            return new ValidationResult($"O valor deve ser um dos seguintes: {valores}.");
        }

        return ValidationResult.Success;
    }
}