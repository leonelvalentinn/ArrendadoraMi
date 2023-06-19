using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ArrendadoraM.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using ArrendadoraM.Controllers;
using System.Security.Claims;

namespace ArrendadoraM.Controllers;

//[Authorize]
public class ArrendadoraController : Controller
{
    private readonly IConfiguration _conf;

    public ArrendadoraController(IConfiguration conf)
    {
        this._conf = conf;
    }

    [Authorize(Roles="1")]
    public IActionResult Index()
    {
        DataTable tbl = new DataTable();
        using (MySqlConnection cnx= new MySqlConnection(_conf.GetConnectionString("DevConnection"))){
            cnx.Open();
            string query = "SELECT * FROM maquinaria";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx);
            adp.Fill(tbl);
        }
        return View(tbl);
    }

   [Authorize(Roles="1")]
    public IActionResult RegistroClientes()
    {
        return View();
    }

   [Authorize(Roles="1")]
    public IActionResult Ventas()
    {
        return View();
    }

    public IActionResult Maquinaria()
    {
        return View();
    }

   [Authorize(Roles="1")]
    public IActionResult Personal()
    {
        return View();
    }

   [Authorize(Roles="1")]
    public IActionResult Eliminar(int? id) {
            using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection"))){
            cnx.Open();
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = cnx;
            comm.CommandText = "DELETE FROM maquinaria WHERE ID_Maquinaria = '" + id.ToString() + "'";
            comm.ExecuteNonQuery();
            cnx.Close();
            }
        return View();
    }

    public IActionResult AgregarMaquina() {
        return View();
    }

//Formulario se utilizara para editar y agregar nuevo
   [Authorize(Roles="1")]
    public IActionResult Formulario(int? id) { //<SE declara que se puede recibir o no un id
        //Sihay id=> editar, si no hay => agregar
        string Modelo=" ", Num_serie=" ", Marca=" ", Tipo, Capacidad, Estado, Combustible;
        int Stock=0;
        if(HttpMethods.IsPost(Request.Method)) {
            Modelo = Convert.ToString(Request.Form["Modelo"]);
            Num_serie = Convert.ToString(Request.Form["Num_serie"]);
            Marca = Convert.ToString(Request.Form["Marca"]);
            Tipo = Convert.ToString(Request.Form["Tipo"]);
            Capacidad = Convert.ToString(Request.Form["Capacidad"]);
            Estado = Convert.ToString(Request.Form["Estado"]);
            Combustible = Convert.ToString(Request.Form["Combustible"]);
            Stock = Convert.ToInt32(Request.Form["Stock"]);

            if(id>0) {
                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection"))){
                cnx.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.Connection = cnx;
                comm.CommandText = "UPDATE maquinaria SET Modelo=?Modelo, Num_Serie=?Num_Serie, Marca=?Marca, Tipo=?Tipo, Capacidad=?Capacidad, Estado=?Estado, Combustible=?Combustible, Stock=?Stock WHERE ID_Maquinaria = '" + id.ToString() + "'";
                comm.Parameters.Add("?Modelo", MySqlDbType.VarChar).Value = Modelo;              
                comm.Parameters.Add("?Num_Serie", MySqlDbType.VarChar).Value = Num_serie;
                comm.Parameters.Add("?Marca", MySqlDbType.VarChar).Value = Marca;
                comm.Parameters.Add("?Tipo", MySqlDbType.VarChar).Value = Tipo;
                comm.Parameters.Add("?Capacidad", MySqlDbType.VarChar).Value = Capacidad;
                comm.Parameters.Add("?Estado", MySqlDbType.VarChar).Value = Estado;
                comm.Parameters.Add("?Combustible", MySqlDbType.VarChar).Value = Combustible;
                comm.Parameters.Add("?Stock", MySqlDbType.Int32).Value = Stock;
                comm.ExecuteNonQuery();
                cnx.Close(); 
                }
            } else {
               using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection"))){
                cnx.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.Connection = cnx;
                comm.CommandText = "INSERT INTO maquinaria (Modelo, Num_Serie, Marca, Tipo, Capacidad, Estado, Combustible, Stock) VALUES (?Modelo, ?Num_Serie, ?Marca, ?Tipo, ?Capacidad, ?Estado, ?Combustible, ?Stock)";
                comm.Parameters.Add("?Modelo", MySqlDbType.VarChar).Value = Modelo;
                comm.Parameters.Add("?Num_Serie", MySqlDbType.VarChar).Value = Num_serie;
                comm.Parameters.Add("?Marca", MySqlDbType.VarChar).Value = Marca;
                comm.Parameters.Add("?Tipo", MySqlDbType.VarChar).Value = Tipo;
                comm.Parameters.Add("?Capacidad", MySqlDbType.VarChar).Value = Capacidad;
                comm.Parameters.Add("?Estado", MySqlDbType.VarChar).Value = Estado;
                comm.Parameters.Add("?Combustible", MySqlDbType.VarChar).Value = Combustible;
                comm.Parameters.Add("?Stock", MySqlDbType.Int32).Value = Stock;
                comm.ExecuteNonQuery();
                cnx.Close();
                }
            }
        }
        ArrendadoraViewModel modelo = new ArrendadoraViewModel();
        DataTable tbl = new DataTable();
        if (id > 0) {
            //recuperar el registro desde la base de datos
        using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection"))){
            cnx.Open();
            string query = "SELECT * FROM maquinaria WHERE ID_Maquinaria = '" + id.ToString() + "'";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, cnx); 
            adp.Fill(tbl);
            cnx.Close();
        } //MySqlCommand(Queryable, cmx)
        modelo.Id = Convert.ToInt32(tbl.Rows[0]["ID_Maquinaria"]);
        modelo.Modelo = tbl.Rows[0]["Modelo"].ToString();
        modelo.Num_serie = tbl.Rows[0]["Num_serie"].ToString();
        modelo.Marca = tbl.Rows[0]["Marca"].ToString();
        modelo.Tipo = tbl.Rows[0]["Tipo"].ToString();
        modelo.Capacidad = tbl.Rows[0]["Capacidad"].ToString();
        modelo.Estado = tbl.Rows[0]["Estado"].ToString();
        modelo.Combustible = tbl.Rows[0]["Combustible"].ToString();
        modelo.Stock = Convert.ToInt32(tbl.Rows[0]["Stock"]);
        }
        return View(modelo);
    }

    public IActionResult Error(){
        return View();
    }

   [Authorize(Roles="1")]
    public IActionResult IndexLogin(){
        return View();
    }

    public IActionResult Servicios(){
        return View();
    }
}
