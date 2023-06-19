using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ArrendadoraM.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ArrendadoraM.Models;

public class Usuarios {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
    public string IdRol { get; set; }
}