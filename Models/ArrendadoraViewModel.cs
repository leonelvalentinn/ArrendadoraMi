using System.ComponentModel.DataAnnotations; //Para más info entrar a la documentación de DataAnnotations
using MySql.Data.MySqlClient;

namespace ArrendadoraM.Models;

public class ArrendadoraViewModel {
    [Key] //Se utiliza para nonbrar al atributo de abajo como una PK
    public int Id { get; set; }
    [Required] //Se utiliza para decir que el atributo de abajo es obligatorio
    public string Modelo { get; set; }
    public string Num_serie { get; set; }
    public string Marca { get; set; }
    public string Tipo { get; set; }
    public string Capacidad { get; set; }
    public string Estado { get; set; }
    public string Combustible { get; set; }
    public int Stock { get; set; }

}