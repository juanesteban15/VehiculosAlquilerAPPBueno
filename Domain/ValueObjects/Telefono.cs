using System;
using System.Collections.Generic;
using System.Text;
namespace VehiculosAlquilerApp.Domain.ValueObjects;


public class Telefono
{
    public string Numero { get; private set; }
    public string Tipo { get; private set; }

    public Telefono(string numero, string tipo)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new Exception("Número requerido");

        if (numero.Length < 7)
            throw new Exception("Número inválido");

        if (tipo != "personal" && tipo != "trabajo" && tipo != "emergencia")
            throw new Exception("Tipo inválido");

        Numero = numero;
        Tipo = tipo;
    }
}